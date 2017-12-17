using Abp.Application.Services;

namespace NetCommunitySolution.Security
{
    public interface ICaptchaService: IApplicationService
    {

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="imageWidth"></param>
        /// <param name="imageHeight"></param>
        /// <returns></returns>
        byte[] GetValidationImage(out string code, int imageWidth = 200, int imageHeight = 45);
    }
}
