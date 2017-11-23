function setUpModule(module, definitions) {
  definitions.forEach(x => {
    module[x.type](x.name, x.value)
  });
}

export default setUpModule;
