using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Roomie.Web.Website.Helpers
{
    public class CacheCookie
    {
        private const string cacheCookieName = "cache";
        private Dictionary<string, string> _files;
        private List<string> _filesNotInCache; 

        public CacheCookie(HttpCookie cookie)
        {
            _files = new Dictionary<string, string>();
            _filesNotInCache = new List<string>();

            if (cookie == null)
            {
                return;
            }

            foreach (var key in cookie.Values.AllKeys)
            {
                var value = cookie.Values[key];
                if (!string.IsNullOrWhiteSpace(value))
                {
                    try
                    {
                        _files.Add(key, value);
                    }
                    catch
                    {
                    }
                    
                }
            }
        }

        private bool IsFileCached(string name, string version)
        {
            name = name.ToLowerInvariant();
            version = version.ToLowerInvariant();

            if (!_files.ContainsKey(name))
            {
                return false;
            }

            if (!string.Equals(_files[name], version))
            {
                return false;
            }

            return true;
        }

        private void GetNameAndVersion(string path, out string name, out string version)
        {
            var start = path.LastIndexOf("/");
            var middle = path.IndexOf("?");

            name = path.Substring(start + 1, middle - start - 1);
            version = path.Substring(middle + 3);

            name = name.ToLowerInvariant();
            version = version.ToLowerInvariant();

            version = version.GetHashCode().ToString();
        }

        public bool IsFileCached(string path)
        {
            string name;
            string version;
            GetNameAndVersion(path, out name, out version);

            return IsFileCached(name, version);
        }

        private void SetFile(string name, string version)
        {
            name = name.ToLowerInvariant();
            version = version.ToLowerInvariant();

            if (_files.ContainsKey(name))
            {
                _files.Remove(name);
            }

            _files.Add(name, version);
        }

        public void SetFile(string path)
        {
            string name;
            string version;
            GetNameAndVersion(path, out name, out version);

            SetFile(name, version);
        }

        public HttpCookie ToHttpCookie()
        {
            var cookie = new HttpCookie(cacheCookieName);

            foreach (var entry in _files)
            {
                cookie.Values.Add(entry.Key, entry.Value);
            }

            return cookie;
        }

        public void Save(HttpResponseBase response)
        {
            var cookie = ToHttpCookie();
            cookie.Expires = DateTime.UtcNow.AddYears(10);

            response.SetCookie(cookie);
        }

        public IEnumerable<string> FilesNotInCache
        {
            get
            {
                return _filesNotInCache;
            }
        }

        public void AddAFileForDownload(string path)
        {
            _filesNotInCache.Add(path);
        }

        public static CacheCookie FromRequest(HttpRequestBase request)
        {
            var cookie = request.Cookies[cacheCookieName];
            var result = new CacheCookie(cookie);

            return result;
        }
    }
}