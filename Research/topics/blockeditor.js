var theSelection = false;

var clientPC = navigator.userAgent.toLowerCase();
var clientVer = parseInt(navigator.appVersion);

var is_ie = ((clientPC.indexOf("msie") != -1) && (clientPC.indexOf("opera") == -1));
var is_win = ((clientPC.indexOf("win")!=-1) || (clientPC.indexOf("16bit") != -1));

helplines = new Array(
 "Абзац: &lt;p&gt;текст&lt;/p&gt;",
 "Перевод строки: &lt;br&gt;текст",
 "Жирный текст: &lt;b&gt;текст&lt;/b&gt;",
 "Наклонный текст: &lt;i&gt;текст&lt;/i&gt;",
 "Подчёркнутый текст: &lt;u&gt;текст&lt;/u&gt;",
 "Перечёркнутый текст: &lt;s&gt;текст&lt;/s&gt;",
 "Нижний индекс: &lt;sub&gt;текст&lt;/sub&gt;",
 "Верхний индекс: &lt;sup&gt;текст&lt;/sup&gt;",
 "Центрировать: &lt;div align=center&gt;текст&lt;/div&gt;",
 "Цвет шрифта: &lt;font color=red&gt;текст&lt;/font&gt;  Подсказка: или color=#FF0000",
 "Код: &lt;code&gt;текст&lt;/code&gt;",
 "Листинг (программа): &lt;pre&gt;код&lt;/pre&gt;",
 "Цитата: &lt;blockquote&gt;текст&lt;/blockquote&gt;",
 "Знаки &lt; и &gt; в тексте страницы",
 "Маркированный список: &lt;ul&gt;текст&lt;/ul&gt;",
 "Нумерованный список: &lt;ol&gt;текст&lt;/ol&gt;",
 "Номер или маркер в списке: &lt;li&gt;текст",
 "Вставить картинку: &lt;img src=http://image_url&gt;",
 "Вставить ссылку: &lt;a href=http://url&gt;текст ссылки&lt;/a&gt;",
 "Адрес E-mail: &lt;a href=mailto:E-mail&gt;E-mail&lt;/a&gt;"
);

bbcode = new Array();
bbtags = new Array(
 '[B]','[/B]',
 '[I]','[/I]',
 '[U]','[/U]',
 '[S]','[/S]',
 '[SUB]','[/SUB]',
 '[SUP]','[/SUP]',
 '[LEFT]','[/LEFT]',
 '[CENTER]','[/CENTER]',
 '[RIGHT]','[/RIGHT]',
 '[QUOTE]','[/QUOTE]',
 '[OFFTOP]','[/OFFTOP]',
 '[CODE]','[/CODE]',
 '[SPOILER]','[/SPOILER]',
 '[HIDE]','[/HIDE]'
);

// тут перечисляем индексы тэгов, которые не надо закрывать
function not_closed_tags(n) {
 var r=false;
 //if (n==0 || n==2 || n==32 || n==34) r=true;
 return r;
}

function helpline (i) {
 if (i<0) document.getElementById('helpbox').innerHTML =  'Можно быстро применить стили к выделенному тексту';
 else document.getElementById('helpbox').innerHTML =  helplines[i];
}

function getarraysize(thearray) {
 for (i = 0; i < thearray.length; i++) {
  if ((thearray[i] == "undefined") || (thearray[i] == "") || (thearray[i] == null)) return i;
 }
 return thearray.length;
}

function arraypush(thearray,value) {
 thearray[ getarraysize(thearray) ] = value;
}

function arraypop(thearray) {
 thearraysize = getarraysize(thearray);
 retval = thearray[thearraysize - 1];
 delete thearray[thearraysize - 1];
 return retval;
}

function bbplace(text) {
 var txtarea = document.f1.Post;
 var scrollTop = (typeof(txtarea.scrollTop) == 'number' ? txtarea.scrollTop : -1);
 if (txtarea.createTextRange && txtarea.caretPos) {
  var caretPos = txtarea.caretPos;
  caretPos.text = caretPos.text.charAt(caretPos.text.length - 1) == ' ' ? caretPos.text + text + ' ' : caretPos.text + text;
  txtarea.focus();
 } 
 else if (txtarea.selectionStart || txtarea.selectionStart == '0') {
  var startPos = txtarea.selectionStart;
  var endPos = txtarea.selectionEnd;
  txtarea.value = txtarea.value.substring(0, startPos) + text + txtarea.value.substring(endPos, txtarea.value.length);
  txtarea.focus();
  txtarea.selectionStart = startPos + text.length;
  txtarea.selectionEnd = startPos + text.length;
 }
 else {
  txtarea.value  += text;
  txtarea.focus();
 }
 if (scrollTop >= 0 ) { txtarea.scrollTop = scrollTop; }
}

function bbstyle(bbnumber) {
 var txtarea = document.f1.Post;
 txtarea.focus();
 donotinsert = false;
 theSelection = false;
 bblast = 0;
 if (bbnumber == -1) { //Закрыть все теи
  while (bbcode[0]) {
   butnumber = arraypop(bbcode) - 1;
   txtarea.value += bbtags[butnumber + 1];
  }
  txtarea.focus();
  return;
 }
 if ((clientVer >= 4) && is_ie && is_win) {
  theSelection = document.selection.createRange().text; //Получить выделение для IE
  if (theSelection) { //Добавить теги вокруг непустого выделения
   document.selection.createRange().text = bbtags[bbnumber] + theSelection + bbtags[bbnumber+1];
   txtarea.focus();
   theSelection = '';
   return;
  }
 }
 else if (txtarea.selectionEnd && (txtarea.selectionEnd - txtarea.selectionStart > 0)) {
  //Получить выделение для Mozilla
  mozWrap(txtarea, bbtags[bbnumber], bbtags[bbnumber+1]);
  return;
 }
 for (i = 0; i < bbcode.length; i++) {
  if (bbcode[i] == bbnumber+1 && !not_closed_tags(bbnumber)) { 
   bblast = i;
   donotinsert = true;
  }
 }
 if (donotinsert) {
  while (bbcode[bblast]) {
   butnumber = arraypop(bbcode) - 1;
   if (!not_closed_tags(butnumber)) bbplace(bbtags[butnumber + 1]);
  }
  txtarea.focus();
  return;
 } 
 else { //Открыть тег
  bbplace(bbtags[bbnumber]);
  arraypush(bbcode,bbnumber+1);
  txtarea.focus();
  return;
 }
 storeCaret(txtarea);
}

function mozWrap(txtarea, open, close) {
 if (txtarea.selectionEnd > txtarea.value.length) { txtarea.selectionEnd = txtarea.value.length; }
 var oldPos = txtarea.scrollTop;
 var oldHght = txtarea.scrollHeight;
 var selStart = txtarea.selectionStart;
 var selEnd = txtarea.selectionEnd+open.length;
 txtarea.value = txtarea.value.slice(0,selStart)+open+txtarea.value.slice(selStart); 
 txtarea.value = txtarea.value.slice(0,selEnd)+close+txtarea.value.slice(selEnd);
 txtarea.selectionStart = selStart+open.length;
 txtarea.selectionEnd = selEnd;
 var newHght = txtarea.scrollHeight - oldHght;
 txtarea.scrollTop = oldPos + newHght;
 txtarea.focus();
}

function storeCaret(textEl) { //Вставка в позицию каретки - патч для IE
 if (textEl.createTextRange) textEl.caretPos = document.selection.createRange().duplicate();
 document.getElementById('helpbox').innerHTML = "Всего: "+ document.f1.Post.value.length;
}

function showIcons (moderator, theme) {
    var l=bbtags.length;


    for (i=0; i<l; i+=2) {
        var p = bbtags[i].indexOf(' ');
        if (p<0) p = bbtags[i].indexOf(']');
        if (p<0) p = bbtags[i].indexOf(';');
        var tagname = bbtags[i].substring (1,p).toLowerCase();
        var i2= i/2;
        var alter= helplines[i2];
        document.writeln ('<img src="file:///android_asset/forum/style_images/1/folder_editor_buttons_'+theme+'/'+tagname+'.png" width="20" height="20" hspace="0" vspace="0" alt="'+alter+'" title="'+alter+'"  onClick="bbstyle('+i+')" />');
  //if (i==14 || i==26 || i==32) document.writeln ('&nbsp;&nbsp;&nbsp;');
    }
}

var maxLen=1024;
var maxWordLen=80;

function strip_tags (string) {
 return string.replace(/<\/?[^>]+>/gi, '');  
} 

function trim(string) {
 return string.replace (/(^\s+)|(\s+$)/g, "");
}

function goodWordsLength (v) {
 var s=v.split(/\s/);
 for (var i=0; i<s.length; i++) 
  if (s[i].length>maxWordLen) {
   return false;
  }
 return true;
}

function checkblock() {
 var text=strip_tags(trim(document.f1.Post.value));
 if (text=='') {
  window.alert (
   'Текст блока не может быть пустым, пожалуйста, заполните его');
  return false;
 }
 if (text.length > maxLen) {
  window.alert (
   'Текст слишком длинный. Допустимая длина: '+ maxLen);
  return false;
 }
 if (goodWordsLength(text)==false) {
  window.alert (
   'В тексте есть слишком длинные слова. Допустимая длина: '+
    maxWordLen);
  return false;
 }
 return true;
}