using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp1.Utilities
{
    public class DocLib
    {
        public static readonly string Resolution = "Nghị quyết";
        public static readonly string Decision = "Quyết định";
        public static readonly string Self_Criticism = "Tự kiểm điểm";
        public static readonly string Commitment = "Cam kết nội quy";
        public static readonly string PartyDevelope = "Kế hoạch phát triển Đảng";
        public static readonly string Another = "Khác";
        public List<string> GetEnumerator()
        {
            var list = this.GetType().GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(f => f.FieldType == typeof(string))
                .Select(f => f.GetValue(this).ToString()).ToList();
            list.Insert(0, null);
            return list;
        }
    }
}
