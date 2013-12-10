var createNamespace = function (name, parent) {
    parent = parent || window;
    var parts = name.split('.');

    while (parts.length > 0) {
        var part = parts[0];
        parts.shift();
        parent = parent[part] = parent[part] || {};
    }

    return parent;
};
