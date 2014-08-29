using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IISManager.Models
{
    public class IISSiteEntity
    {
        /// <summary>
        /// 网站名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 网站ID
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 物理路径
        /// </summary>
        public string PhysicalPath { get; set; }
        /// <summary>
        /// 运行状态
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// 应用程序池
        /// </summary>
        public string Pool { get; set; }

        public List<string> List_url { get; set; }
    }
}