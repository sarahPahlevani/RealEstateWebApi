namespace RealEstateAgency.Dtos.Other
{
    public class MessageResultDto<TResponse> : IMessageResultDto<TResponse>
    {
        public bool Ok { get; set; }
        public TResponse Message { get; set; }
        public TResponse ErrorMessage { get; set; }
    }

    public class MessageResultDto : IMessageResultDto<string>
    {
        public virtual bool Ok { get; set; }
        public string Message { get; set; }
        public string ErrorMessage { get; set; }
    }
    public class CheckUserResultDto : MessageResultDto
    {
        public override bool Ok => !EmailHasProblem && !UsernameHasProblem;
        public bool EmailHasProblem { get; set; }
        public bool UsernameHasProblem { get; set; }
    }
}