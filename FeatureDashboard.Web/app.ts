import './styles.scss';

import * as $ from 'jquery';

import './font-awesome-registration';

import { applicationEnvironmentsModule } from './Pages/ApplicationEnvironments/application-environments';
import { applicationsModule } from './Pages/Applications/applications';

applicationEnvironmentsModule.run();
applicationsModule.run();

$(() => {
  applicationEnvironmentsModule.ready($);
  applicationsModule.ready($);
});
