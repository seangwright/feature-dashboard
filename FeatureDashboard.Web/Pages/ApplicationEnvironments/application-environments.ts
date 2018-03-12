import { IJQueryModule } from 'jquery-module';

function run() {
  const log = (msg: any) =>
    console.log(`This is from application-environments.ts : ${msg}`);

  log('test');
}

function ready($: JQueryStatic) {}

export const applicationEnvironmentsModule: IJQueryModule = { run, ready };
