using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sys.PanGuLucene
{
    public class TestSearch
    {
        public string html_content{get;set;}
        public void Test() {
            //PanGuLuceneHelper.instance.DeleteAll();//删除全部

            //PanGuLuceneHelper.instance.Delete("1d");//根据id删除
            bool exec = false;
            if (exec)
            {
                List<MySearchUnit> list = new List<MySearchUnit>();
                list.Add(new MySearchUnit("1a", "标题小王", "今天是小王的生日，大家都很高兴去他家喝酒，玩了一整天。", new Random().Next(1, 10).ToString(), "", ""));
                list.Add(new MySearchUnit("1b", "标题小张", "今天是小张的生日，大家都很高兴去他家喝酒，玩了几天。", new Random().Next(1, 10).ToString(), "", ""));
                list.Add(new MySearchUnit("1c", "标题小王", "今天是小王的生日，大家都很高兴去他家喝酒，玩了一整天。", new Random().Next(1, 10).ToString(), "", ""));
                list.Add(new MySearchUnit("1d", "标题小张", "今天是小张的生日，大家都很高兴去他家喝酒，玩了几天。", new Random().Next(1, 10).ToString(), "", ""));
                PanGuLuceneHelper.instance.CreateIndex(list);//添加索引
            }
            int count = 0;
            int PageIndex = 2;
            int PageSize = 4;
            html_content = "";
            List<MySearchUnit> searchlist = PanGuLuceneHelper.instance.Search("3", "小王 生日", PageIndex, PageSize, out count);
            html_content += ("查询结果：" + count + "条数据<br/>");
            if (searchlist == null || searchlist.Count == 0)
            {
                html_content += ("未查询到数据。<br/>");
            }
            else
            {
                foreach (MySearchUnit data in searchlist)
                {
                    html_content += (string.Format("id：{0},title：{1},content：{2},flag：{3},updatetime：{4}<br/>", data.id, data.title, data.content, data.flag, data.updatetime));
                }
            }
            html_content += (PanGuLuceneHelper.instance.version); 
        }
    }
}
