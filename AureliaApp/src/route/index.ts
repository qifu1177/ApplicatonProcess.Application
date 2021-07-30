import { RouterConfiguration } from 'aurelia-router';
import { PLATFORM } from "aurelia-pal";
export class AppRouter {

    public SetConfigureRouter(config: RouterConfiguration): void {
        config.title = 'Aurelia';
        config.options.pushState = true;
        config.options.root = '/';
        config.map([
            { route: ['', 'users'], name: 'users', moduleId: PLATFORM.moduleName("views/users"), nav: true },
            { route: 'user/create', name: 'user create', moduleId: PLATFORM.moduleName('views/user'), nav: true },
            { route: 'user/edit/:email', name: 'user edit', moduleId: PLATFORM.moduleName('views/user') },
            { route: 'assets/:email', name: 'assets', moduleId: PLATFORM.moduleName('views/assets') },
            { route: 'asset/:email/create', name: 'asset create', moduleId: PLATFORM.moduleName('views/asset') },
            { route: 'asset/:email/edit/:id', name: 'asset edit', moduleId: PLATFORM.moduleName('views/asset') }
        ]);
    }
}


