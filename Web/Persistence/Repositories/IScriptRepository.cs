﻿using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories
{
    public interface IScriptRepository
    {
        ScriptModel Get(int id);
        Page<ScriptModel> List(ListFilter filter);
        void Add(ScriptModel script);
        void Remove(ScriptModel script);
    }
}
