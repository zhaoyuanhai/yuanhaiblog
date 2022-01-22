namespace BasicModel
{
    public abstract class ServiceBase
    {
        public ServiceResult<T> Result<T>(T data)
        {
            return new ServiceResult<T>(data);
        }
    }
}
