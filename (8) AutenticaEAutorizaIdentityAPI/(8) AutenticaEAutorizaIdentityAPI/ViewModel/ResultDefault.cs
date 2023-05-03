namespace _8__AutenticaEAutorizaIdentityAPI.ViewModel
{
    public class ResultDefault <T>
    {
        public T Data { get; set; }
        public List<string> Errors { get; set; } = new();

        public ResultDefault(T data, List<string> errors)
        {
            Data = data;
            Errors = errors;
        }
        public ResultDefault(T data)
            =>Data = data;
        public ResultDefault(List<string> errors)
            => Errors = errors;

        public ResultDefault(string erro)
            => Errors.Add(erro);
    }
}
