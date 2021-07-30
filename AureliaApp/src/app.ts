import { RouterConfiguration, Router } from 'aurelia-router';
import { AppRouter } from './route';
import { Store } from 'aurelia-store';
import { State } from 'state';
import { HttpClient } from 'aurelia-fetch-client';
import { Config } from 'config';
import { inject } from 'aurelia-framework';
import { DialogService } from 'aurelia-dialog';
import { I18N } from 'aurelia-i18n';
import { Question } from './components/question';
import { QuestionRequste } from './models/questionrequste'
@inject(DialogService, I18N, Store)

export class App {
  router: Router;
  i18n: I18N;
  dialogService: DialogService;

  currentLN: string;
  state: State;
  constructor(dialogService: DialogService, i18n: I18N, private store: Store<State>) {
    this.dialogService = dialogService;
    this.i18n = i18n;

    this.currentLN = "en";
  }
  bind() {
    let that=this;
    this.store.state.subscribe(
      (state) => {
        that.state = state;
        that.state.currentLN[0] = that.currentLN;
        that.loadAssetNames();
      }
    );

  }
  configureRouter(config: RouterConfiguration, router: Router): void {
    this.router = router;
    let appRouter: AppRouter = new AppRouter();
    appRouter.SetConfigureRouter(config);

  }
  loadAssetNames(): void {
    let httpClient: HttpClient = new HttpClient();

    httpClient.fetch(`${Config.BASE_URL}AssetNames/${this.currentLN}`)
      .then(async response => {
        let data = await response.json();
        if (Array.isArray(data)) {
          this.state.assetnames.splice(0, this.state.assetnames.length);
          for (let s of data)
            this.state.assetnames.push(s);
        } else {
          throw data.message;
        }
      })
      .catch(error => {
        let questionRequest: QuestionRequste = new QuestionRequste(error, this.i18n.tr('app:tryagain'));
        this.dialogService.open({ viewModel: Question, model: questionRequest, lock: true }).whenClosed(response => {
          if (!response.wasCancelled) {
            this.loadAssetNames();
          } else {
          }
        });
      });
  }
  setLocale(language: string): void {
    this.i18n.setLocale(language);
    this.currentLN = language;
    this.state.currentLN[0] = this.currentLN;
  }
}


