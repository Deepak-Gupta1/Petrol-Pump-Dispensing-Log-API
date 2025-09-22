namespace FuelDispensingAPI.Domain
{
    public class ResponseModel<T>
    {
        public bool sucess { get; set; }

        public T data { get; set; }

        public string message { get; set; }

        public ResponseModel(bool sucess, T data, string message)
        {
            this.sucess = sucess;
            this.data = data;
            this.message = message;
        }
    }
}
