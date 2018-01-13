function defineModule({
  config = [],
  definitions = [],
  dependencies = [],
  name,
}) {
  var module = angular.module(
    name,
    dependencies.map(x => x.name || x)
  );

  config.forEach(x => module.config(x));

  definitions.forEach(x => module[x.type](x.name, x.value));

  return module;
};

export default defineModule;
