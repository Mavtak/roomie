function setUpModule(module, definitions) {
  definitions.forEach(x => {
    console.log(module.name, x.type, x.name);

    module[x.type](x.name, x.value)
  });
}

export default setUpModule;
