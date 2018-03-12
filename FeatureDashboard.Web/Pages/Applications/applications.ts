import { IJQueryModule } from 'jquery-module';

function run() {
  console.log('in-run');
}

function ready($: JQueryStatic) {
  if (!$) {
    return;
  }

  const users = $('.users');

  if (!users) {
    return;
  }

  users.css('background-color', 'red');
}

export const applicationsModule: IJQueryModule = { run, ready };
