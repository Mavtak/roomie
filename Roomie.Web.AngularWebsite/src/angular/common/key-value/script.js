function keyValue() {

  return {
    restrict: 'E',
    scope: {
      key: '@key',
      value: '@value',
      href: '@href',
      fullWidth: '=fullWidth'
    },
    templateUrl: 'common/key-value/template.html',
  };

};

export default keyValue;
