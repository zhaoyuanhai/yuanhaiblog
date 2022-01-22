using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BasicModel
{
    public class ServiceResult
    {
        public virtual IActionResult Result()
        {
            return new OkResult();
        }
    }

    public class ServiceResult<T> : ServiceResult
    {
        private readonly T _data;

        public T ServiceData => _data;

        public ServiceResult(T data)
        {
            _data = data;
        }

        public override IActionResult Result()
        {
            return new JsonResult(_data);
        }
    }
}
