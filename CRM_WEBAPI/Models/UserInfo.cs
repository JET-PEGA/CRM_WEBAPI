namespace CRM_WEBAPI.Models
{
    /// <summary>
    /// 使用者資訊
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 工號
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 密碼
        /// </summary>
        public string Password { get; set; }
    }
}