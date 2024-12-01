

function scrollToElement(id) {

    var el = elem(id);
    var x = 0;
    var y = 0;

    while (el != null) {
        x += el.offsetLeft;
        y += el.offsetTop;
        el = el.parent;
    }
    window.scrollTo(0, y);

};

function areaPlus(){
   var textarea_obj = elem ( "Post" );
   textarea_obj.rows+=2;
}

function areaMinus(){
   var textarea_obj = elem ( "Post" );
   if(textarea_obj.rows<=8)return;
   textarea_obj.rows-=2;
}

function getSctollPosition(id){
    var elem = document.getElementById(id);
    var x = 0;
    var y = 0;

    while (elem != null) {
        x += elem.offsetLeft;
        y += elem.offsetTop;
        elem = elem.parent;
    }
    window.HTMLOUT.getSctollPosition(y);
}

function getPostBody(){
   var textarea_obj = elem ( "Post" );

   window.HTMLOUT.setPostBody(textarea_obj.value);

};

function clearPostBody(){
   var textarea_obj = elem ( "Post" );

   textarea_obj.value=null;

};

function preparePost(){
   var textarea_obj = elem ( "Post" );

   window.HTMLOUT.post(textarea_obj.value);

};

function advPost(){
   var textarea_obj = elem ( "Post" );

   window.HTMLOUT.advPost(textarea_obj.value);

};

var tags=new Array("b","i","u","s","left","center","right","quote","mod","ex");
var itags=new Array();
function insertText(text)
{
	var textarea_obj = elem ( "Post" );
	textarea_obj.value+=text;
	return false;
}

function getDivInnerText(msgId){
	var textarea_obj = elem ( msgId );
	return textarea_obj.innerText;

}

function postQuote(postId, date, userNick){
	var textarea_obj = elem ("msg" + postId );
	var text=textarea_obj.innerText;
	return insertText("[quote name='" + userNick + "' date='" + date + "' post='" + postId + "']\n" + text + "\n[/quote]" );
}

function addtag(id)
{
	var textarea_obj = elem ( "Post" );
	var n=-1;
	for(i=0;i<tags.length;i++)
	{
		if(tags[i]==id)n=i;
	}
	if(n!=-1)
	{
		if(itags[n]!=1)
		{
			itags[n]=1;
			textarea_obj.value+="["+id+"]";
		}else{
			itags[n]=0;
			textarea_obj.value+="[/"+id+"]";
		}
	}
	return false;
}
function elem ( id )
{
	if ( isdef ( typeof ( document.getElementById ) ) ) return document.getElementById ( id );
	else if ( isdef ( typeof ( document.all ) ) ) return document.all [ id ];
	else if ( isdef ( typeof ( document.layers ) ) ) return document [ id ];
	else return null;
}
function isdef ( typestr )
{
	return ( ( typestr != "undefined" ) && ( typestr != "unknown" ) ) ? true : false;
}
