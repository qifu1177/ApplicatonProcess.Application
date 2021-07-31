import { inject, computedFrom } from 'aurelia-framework';
import { Router } from 'aurelia-router';
import { HttpClient, json } from 'aurelia-fetch-client';
import { DialogService } from 'aurelia-dialog';
//import { Config } from '../config';
import { Question } from '../components/question';
import { QuestionRequste } from '../models/questionrequste';
import { I18N } from 'aurelia-i18n';
import { ValidationRules, ValidationController, ValidationControllerFactory } from 'aurelia-validation';
import { BootstrapFormRenderer } from './bootstrap-form-renderer';
import { Store } from 'aurelia-store';
import { State } from 'state';
@inject(Router, DialogService, I18N, ValidationControllerFactory, Store)

export abstract class DataEDit<T>
{
    router: Router;
    controlName: string;
    parentId: string | number;
    currentId: string | number;
    data: T;
    backUpData: T;
    dialogService: DialogService;
    isNew: Boolean;
    i18n: I18N;
    controller: ValidationController;
    render: BootstrapFormRenderer;
    state: State;


    constructor(router: Router, dialogService: DialogService, i18n: I18N, controllerFactory: ValidationControllerFactory, private store: Store<State>) {
        this.router = router;
        this.dialogService = dialogService;
        this.i18n = i18n;

        this.createNewData();
        this.controller = controllerFactory.createForCurrentScope();
    }

    get disabledSendButton(): boolean {
        return !this.validate();
    }

    get disabledResetButton(): boolean {
        return !this.isChanged();
    }
    bind() {
        this.setValidation();
        this.render = new BootstrapFormRenderer();
        this.controller.addRenderer(this.render);
        let that = this;
        this.store.state.subscribe(
            (state) => {
                that.state = state;
                that.loadData();
            }
        );
    }
    unbind() {
        this.controller.removeRenderer(this.render);
    }
    getUrl(id: string | number = '', forGet: Boolean = false, forPost: Boolean = false): string {
        let url: string = `${this.state.config[0].baseUrl}${this.controlName}`;
        if (forGet) {
            url = `${url}/id`;
        }
        if (forPost) {
            if (this.parentId) {
                url = `${url}/${this.parentId}`;
            }
        } else {
            if (id) {
                url = `${url}/${id}`;
            }
        }
        url = `${url}/${this.state.currentLN[0]}`;
        return url;
    }
    back() {
        //this.router.navigate(`${this.parentRouteName}`);
        this.router.navigateBack();
    }
    abstract createNewData(): void;
    abstract updateData(data: any): void;
    abstract validate(): boolean;
    abstract copyData(source: T, target: T): void;
    abstract isChanged(): boolean;
    abstract setValidation(): void;
    loadDataWithId(): void {
        let httpClient: HttpClient = new HttpClient();
        let url: string = this.getUrl(this.currentId, true, false);

        httpClient.fetch(url)
            .then(async response => {
                let data = await response.json();
                if (data.errorCode) {
                    throw data.message;
                } else {
                    this.updateData(data);
                    this.copyData(this.data, this.backUpData);
                }
            })
            .catch(error => {
                let questionRequest: QuestionRequste = new QuestionRequste(error, this.i18n.tr('app:tryagain'));
                this.dialogService.open({ viewModel: Question, model: questionRequest, lock: true }).whenClosed(response => {
                    if (!response.wasCancelled) {
                        this.loadDataWithId();
                    }
                });
            });
    }
    loadData(): void {
        if (!this.isNew) {
            this.loadDataWithId();
        }
    }
    saveData(): void {
        if (this.validate()) {
            if (this.isNew) {
                this.postData();
            } else {
                this.putData();
            }
        }
    }
    postData(): void {
        let httpClient: HttpClient = new HttpClient();
        let url: string = this.getUrl(this.currentId, false, true);
        let bodyData = json(this.data);
        httpClient.fetch(url, {
            method: 'post',
            body: bodyData
        })
            .then(async response => {
                let data = await response.json();
                if (data.errorCode) {
                    throw data.message;
                } else {
                    this.back();
                }
            })
            .catch(error => {
                let questionRequest: QuestionRequste = new QuestionRequste(error, this.i18n.tr('app:tryagain'));
                this.dialogService.open({ viewModel: Question, model: questionRequest, lock: true }).whenClosed(response => {
                    if (!response.wasCancelled) {
                        this.postData();
                    }
                });
            });
    }
    putData(): void {
        let httpClient: HttpClient = new HttpClient();
        let url: string = this.getUrl(this.currentId, false, false);

        httpClient.fetch(url, {
            method: 'put',
            body: json(this.data)
        })
            .then(async response => {
                let data = await response.json();
                if (data.errorCode) {
                    throw data.message;
                } else {
                    this.back();
                }
            })
            .catch(error => {
                let questionRequest: QuestionRequste = new QuestionRequste(error, this.i18n.tr('app:tryagain'));
                this.dialogService.open({ viewModel: Question, model: questionRequest, lock: true }).whenClosed(response => {
                    if (!response.wasCancelled) {
                        this.putData();
                    }
                });
            });
    }
    reset(): void {
        let questionRequest: QuestionRequste = new QuestionRequste(this.i18n.tr('app:reset_question'), this.i18n.tr('app:OK'));
        this.dialogService.open({ viewModel: Question, model: questionRequest, lock: true }).whenClosed(response => {
            if (!response.wasCancelled) {
                this.copyData(this.backUpData, this.data);
            }
        });
    }
}