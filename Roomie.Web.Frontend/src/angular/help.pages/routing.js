import aboutTemplate from './about.html';
import commandDocumentationTemplate from './command-documentation.html';
import deviceAddressesTemplate from './device-addresses.html';
import hardwareTemplate from './hardware.html';
import indexTemplate from './index.html';

function routing(
  $stateProvider
  ) {

  $stateProvider.state('help', {
    url: '/help',
    template: indexTemplate,
  });

  $stateProvider.state('help/about', {
    url: '/help/about',
    template: aboutTemplate,
  });

  $stateProvider.state('/help/command-documentation', {
    url: '/help/command-documentation',
    controller: 'CommandDocumentationController',
    controllerAs: 'controller',
    template: commandDocumentationTemplate,
  });

  $stateProvider.state('help/device-addresses', {
    url: '/help/device-addresses',
    template: deviceAddressesTemplate,
  });

  $stateProvider.state('help/hardware', {
    url: '/help/hardware',
    template: hardwareTemplate,
  });

}

export default routing;
