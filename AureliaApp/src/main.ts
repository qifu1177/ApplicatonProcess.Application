import { Aurelia } from 'aurelia-framework';
import * as environment from '../config/environment.json';
import { PLATFORM } from 'aurelia-pal';
import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap';
import { initialState } from 'state';
import { I18N, TCustomAttribute } from 'aurelia-i18n';
import Backend from 'i18next-xhr-backend';
import { ValidationMessageProvider } from 'aurelia-validation';
import {jsonLoader} from 'helps/jsonLoader';


export async function configure(aurelia: Aurelia) {
  const Config:any = await jsonLoader('/config.json') ;
  initialState.config.push(Config);
  let language_Setting = Config.languageSetting;
  let setting = localStorage.getItem("languageSetting");
  if (setting) {
    language_Setting = JSON.parse(setting);
    Config.languageSetting.lng=language_Setting.lng;
  }
  aurelia.use
    .standardConfiguration()
    .feature(PLATFORM.moduleName('resources/index'));

  if (environment.debug) {
    aurelia.use.developmentLogging('debug');
  } else {
    aurelia.use.developmentLogging('warn');
    //Config.UpdateBaseUrl();
  }

  if (environment.testing) {
    aurelia.use.plugin(PLATFORM.moduleName('aurelia-testing'));
  }
  aurelia.use.plugin(PLATFORM.moduleName('aurelia-dialog'));
  aurelia.use.plugin(PLATFORM.moduleName('aurelia-store'), { initialState });
  aurelia.use.plugin(PLATFORM.moduleName('aurelia-i18n'), instance => {
    
    let aliases = ['t', 'i18n'];
    TCustomAttribute.configureAliases(aliases);
    instance.i18next.use(Backend);
    return instance.setup({
      backend: {
        loadPath: './locales/{{lng}}/{{ns}}.json'
      },
      ns: language_Setting.ns,
      defaultNS: language_Setting.defaultNS,
      lng: language_Setting.lng,
      fallbackLng: language_Setting.fallbackLng,
      // ns: ["app"],
      // defaultNS: "app",
      // lng: "en",
      // fallbackLng: "de",
      debug: false
    });
  });
  aurelia.use.plugin(PLATFORM.moduleName('aurelia-validation'));
  ValidationMessageProvider.prototype.getMessage = function (key) {
    const i18n = aurelia.container.get(I18N);
    const translation = i18n.tr(`errorMessages.${key}`);
    return this.parser.parse(translation);
  };

  ValidationMessageProvider.prototype.getDisplayName = function (propertyName: string, displayName: string): string {
    const i18n = aurelia.container.get(I18N);
    if (displayName !== null && displayName !== undefined) {
      return i18n.tr(displayName);
    }

    return i18n.tr(propertyName);
  };
  aurelia.start().then(() => aurelia.setRoot(PLATFORM.moduleName('app')));
}
