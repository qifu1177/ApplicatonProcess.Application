import { inject } from 'aurelia-framework';
import { Router } from 'aurelia-router';
import { HttpClient } from 'aurelia-fetch-client';
import { DialogService } from 'aurelia-dialog';
//import { Config } from '../config';
import { Question } from '../components/question';
import { QuestionRequste } from '../models/questionrequste';
import { I18N } from 'aurelia-i18n';
import { Store } from 'aurelia-store';
import { State } from 'state';
@inject(Router, DialogService, I18N, Store)
export abstract class DataGrid<T>
{
    router: Router;
    routeName: string;
    childRouteName: string;
    parentRouteName: string;
    controlName: string;
    parentId: string;
    datas: T[];
    dialogService: DialogService;
    i18n: I18N;
    state: State;

    constructor(router: Router, dialogService: DialogService, i18n: I18N, private store: Store<State>) {
        this.router = router;
        this.dialogService = dialogService;
        this.datas = [];
        this.i18n = i18n;
    }
    bind() {
        let that=this;
        this.store.state.subscribe(
            (state) => {
                that.state = state;
                that.loadDatas();
            }
        );
    }
    getUrl(id: string | number = ''): string {
        let url: string = `${this.state.config[0].baseUrl}${this.controlName}`;
        if (this.parentId) {
            url = `${url}/${this.parentId}`;
        }
        if (id) {
            url = `${url}/${id}`;
        }
        url = `${url}/${this.state.currentLN[0]}`;
        return url;
    }
    back() {
        // this.router.navigate(`${this.parentRouteName}`);
        this.router.navigateBack();
    }
    loadDatas(): void {
        let httpClient: HttpClient = new HttpClient();
        let url: string = this.getUrl();

        httpClient.fetch(url)
            .then(async response => {
                let data = await response.json();
                if (Array.isArray(data)) {
                    this.updateDatas(data);
                } else {
                    throw data.message;
                }
            })
            .catch(error => {
                let questionRequest: QuestionRequste = new QuestionRequste(error, this.i18n.tr('app:tryagain'));
                this.dialogService.open({ viewModel: Question, model: questionRequest, lock: true }).whenClosed(response => {
                    if (!response.wasCancelled) {
                        this.loadDatas();
                    } else {
                    }
                });
            });
    }
    abstract updateDatas(data): void;
    createData(): void {
        this.router.navigate(`${this.routeName}/create`);
    }
    deleteData(id: string | number): void {
        let questionRequest: QuestionRequste = new QuestionRequste(this.i18n.tr('app:delete_data_question'), this.i18n.tr('app:confirm'));
        this.dialogService.open({ viewModel: Question, model: questionRequest, lock: true }).whenClosed(response => {
            if (!response.wasCancelled) {
                this.delete(id);
            } else {
            }
        });
    }
    delete(id: string | number) {
        let httpClient: HttpClient = new HttpClient();
        let url: string = this.getUrl(id);
        httpClient.fetch(url, {
            method: 'delete'
        })
            .then(async response => {
                let data = await response.json();
                if (data.result == 'OK') {
                    this.loadDatas();
                } else {
                    throw data.message;
                }
            })
            .catch(error => {
                let questionRequest: QuestionRequste = new QuestionRequste(error, this.i18n.tr('app:tryagain'));
                this.dialogService.open({ viewModel: Question, model: questionRequest, lock: true }).whenClosed(response => {
                    if (!response.wasCancelled) {
                        this.delete(id);
                    }
                });
            });
    }
    editData(id: string | number): void {
        this.router.navigate(`${this.routeName}/edit/${id}`);
    }
    showChilds(id: string | number): void {
        this.router.navigate(`${this.childRouteName}/${id}`);
    }
}