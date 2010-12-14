function doIframe() {
    o = document.getElementsByTagName('iframe');
    for (i = 0; i < o.length; i++) {
        if (/\bautoHeight\b/.test(o[i].className)) {
            setHeight(o[i]);
            addEvent(o[i], 'load', doIframe);
        }
    }
}

function setHeight(e) {

   // $(e).height = $(e).contents().height;

   // $(e).height($(e).contents().height());
    if (e.contentDocument) {
        e.height = e.contentDocument.body.offsetHeight + 35;
    } else {
        e.height = e.contentWindow.document.body.scrollHeight;
    }
}

function addEvent(obj, evType, fn) {
    if (obj.addEventListener) {
        //obj.detachEvent('on' + evType, fn);
        $(obj).unbind("on" + evType, fn);
        
        
        $(obj).bind(evType, fn, false);
        return true;
    } else if (obj.attachEvent) {
        var r = obj.bind("on" + evType, fn);
        return r;
    } else {
        return false;
    }
}

if (document.getElementById && document.createTextNode) {
    addEvent(window, 'load', doIframe);
}