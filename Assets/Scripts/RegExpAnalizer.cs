using System.Collections.Generic;
using System.Text.RegularExpressions;

public static class RegExpAnalizer{

    //ejemplo de entrada de string
    //Valor Invariante 1 = 4.64115e+029
    public static List<string> MapStrings(string content,string pattern){
        int i;
        List<string> ret = new List<string>();
        MatchCollection view = Regex.Matches(content, pattern);
        for (i = 0; i < view.Count; i++){
            if (view[i].Groups[1].Value != "")
                ret.Add(view[i].Groups[1].Value);
        }
        return ret;
    }

}
