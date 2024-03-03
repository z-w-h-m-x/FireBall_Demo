mergeInto(LibraryManager.library,{

    GetCookie: function(str)
    {
        var cookieValue = null;
        var cookies = document.cookie.split(";");
        for (var i=0; i < cookies.length; i++){
            var cookie = cookies[i].trim();
            if ( cookie.indexOf(str + '=') === 0) {
                cookieValue = cookie.substring(str.length + 1, cookie.length);
                break;
            }
        }
        return cookieValue;
    },

    SetCookie: function(_index,_value)
    {
        document.cookie = UTF8ToString(_index) + "=" + UTF8ToString(_value);
    }

}
)