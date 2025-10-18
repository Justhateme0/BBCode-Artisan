using System.Text;
using System.Text.RegularExpressions;

namespace BBCode_Artisan
{
    public class BBCodeConverter
    {
        public string ConvertToHtml(string bbCode)
        {
            if (string.IsNullOrEmpty(bbCode))
                return string.Empty;

            var html = bbCode;

            html = Regex.Replace(html, @"\[b\](.*?)\[/b\]", "<strong>$1</strong>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"\[i\](.*?)\[/i\]", "<em>$1</em>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"\[u\](.*?)\[/u\]", "<u>$1</u>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"\[s\](.*?)\[/s\]", "<s>$1</s>", RegexOptions.Singleline | RegexOptions.IgnoreCase);

            html = Regex.Replace(html, @"\[color=(.*?)\](.*?)\[/color\]", "<span style=\"color:$1\">$2</span>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"\[size=(.*?)\](.*?)\[/size\]", "<span style=\"font-size:$1px\">$2</span>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"\[font=(.*?)\](.*?)\[/font\]", "<span style=\"font-family:'$1'\">$2</span>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"\[bgcolor=(.*?)\](.*?)\[/bgcolor\]", "<span style=\"background-color:$1\">$2</span>", RegexOptions.Singleline | RegexOptions.IgnoreCase);

            html = Regex.Replace(html, @"\[url=(.*?)\](.*?)\[/url\]", "<a href=\"$1\" target=\"_blank\">$2</a>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"\[url\](.*?)\[/url\]", "<a href=\"$1\" target=\"_blank\">$1</a>", RegexOptions.Singleline | RegexOptions.IgnoreCase);

            html = Regex.Replace(html, @"\[img\](.*?)\[/img\]", "<img src=\"$1\" style=\"max-width:100%\"/>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"\[img=(.*?)x(.*?)\](.*?)\[/img\]", "<img src=\"$3\" width=\"$1\" height=\"$2\" style=\"max-width:100%\"/>", RegexOptions.Singleline | RegexOptions.IgnoreCase);

            html = Regex.Replace(html, @"\[quote\](.*?)\[/quote\]", "<blockquote style=\"border-left:3px solid #ccc;padding-left:10px;margin:10px 0;color:#666\">$1</blockquote>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"\[quote=(.*?)\](.*?)\[/quote\]", "<blockquote style=\"border-left:3px solid #ccc;padding-left:10px;margin:10px 0;color:#666\"><strong>$1:</strong><br/>$2</blockquote>", RegexOptions.Singleline | RegexOptions.IgnoreCase);

            html = Regex.Replace(html, @"\[code\](.*?)\[/code\]", "<pre style=\"background:#f4f4f4;padding:10px;border-radius:4px;overflow-x:auto\"><code>$1</code></pre>", RegexOptions.Singleline | RegexOptions.IgnoreCase);

            html = Regex.Replace(html, @"\[list\](.*?)\[/list\]", "<ul>$1</ul>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"\[list=1\](.*?)\[/list\]", "<ol>$1</ol>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"\[\*\](.*?)(?=\[\*\]|\[/list\])", "<li>$1</li>", RegexOptions.Singleline | RegexOptions.IgnoreCase);

            html = html.Replace("[br]", "<br/>");
            html = html.Replace("\n", "<br/>");

            return html;
        }

        public string WrapText(string text, string tag, string? value = null)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            if (!string.IsNullOrEmpty(value))
                return $"[{tag}={value}]{text}[/{tag}]";

            return $"[{tag}]{text}[/{tag}]";
        }

        public string InsertTag(string text, int selectionStart, int selectionLength, string tag, string? value = null)
        {
            if (selectionLength > 0)
            {
                var selectedText = text.Substring(selectionStart, selectionLength);
                var wrappedText = WrapText(selectedText, tag, value);
                return text.Remove(selectionStart, selectionLength).Insert(selectionStart, wrappedText);
            }
            else
            {
                var tagText = !string.IsNullOrEmpty(value)
                    ? $"[{tag}={value}][/{tag}]"
                    : $"[{tag}][/{tag}]";
                return text.Insert(selectionStart, tagText);
            }
        }

        public string GenerateHtmlPreview(string html, bool isDarkTheme)
        {
            var bgColor = isDarkTheme ? "#2b2b2b" : "#ffffff";
            var textColor = isDarkTheme ? "#e0e0e0" : "#000000";

            return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <style>
        body {{
            font-family: Arial, sans-serif;
            background-color: {bgColor};
            color: {textColor};
            padding: 15px;
            margin: 0;
        }}
        * {{
            word-wrap: break-word;
        }}
    </style>
</head>
<body>
    {html}
</body>
</html>";
        }
    }
}
