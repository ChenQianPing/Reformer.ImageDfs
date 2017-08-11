using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageDfs.Domain.Entities
{
    public enum ImageStatusEnum
    {
        // 尚未验证服务器地址
        NotActivated,
        // 保存成功
        Successful,
        // 保存失败
        Failure
    }
}
