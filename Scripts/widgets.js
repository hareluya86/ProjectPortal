function appear(domId){
$("#other_sites").fadeIn("slow");

}

function fadeOther(domId){

$("#other_sites").fadeOut("slow");
}



function validForm() {

	var err = 0;
	
	if (document.tellafriend.your_name.value == "") {
		document.getElementById('your_name_error').innerHTML = 'Please enter your name';	
		err++;
	} else {
		document.getElementById('your_name_error').innerHTML = '';
	}
	
	if (document.tellafriend.your_email.value == "") {
		document.getElementById('your_email_error').innerHTML = 'Please enter your email';	
		err++;
	} else {
		document.getElementById('your_email_error').innerHTML = '';
	}
	
	if (document.tellafriend.friends_name.value == "") {
		document.getElementById('friends_name_error').innerHTML = 'Please enter your friends name';	
		err++;
	} else {
		document.getElementById('friends_name_error').innerHTML = '';
	}
	
	if (document.tellafriend.friends_email.value == "") {
		document.getElementById('friends_email_error').innerHTML = 'Please enter your friend email';	
		err++;
	} else {
		document.getElementById('friends_email_error').innerHTML = '';
	}
	
	if (err > 0) {
		return false;	
	} else {
		return true;	
	}
}


function checkStuff() {
	document.getElementById('error_terms').innerHTML = '';
	document.getElementById('error_confirm').innerHTML = '';
	document.getElementById('error_mail').innerHTML = '';
	document.getElementById('blankfirstname').innerHTML = '';
	document.getElementById('blanklastname').innerHTML = '';
	document.getElementById('blankemailaddress').innerHTML = '';

if (document.leads.ba.value == "0") {
	document.getElementById('error_attending').innerHTML = '';
	document.getElementById('error_select').innerHTML = '';
}

	var fls = 0;

	if (document.leads.agree.checked == false) {
		document.getElementById('error_terms').innerHTML = 'Please confirm you accept the competition terms and conditions';
		fls = 1;
	}

	if (document.leads.emailaddress.value != document.leads.currentemail.value) {
		if (document.leads.confirmaddress.value != document.leads.emailaddress.value) {
			document.getElementById('error_confirm').innerHTML = 'Please ensure your email addresses match';
			fls = 1;
		}
	}


if (document.leads.ba.value == "0") {
	if (document.leads.segment.value == "0") {
		document.getElementById('error_select').innerHTML = 'Please select a category that best describes you';
		fls = 1;
	}
}

	if (document.leads.firstname.value == "") {
		document.getElementById('blankfirstname').innerHTML = 'First name cannot be blank';
		fls = 1;
	}

	if (document.leads.surname.value == "") {
		document.getElementById('blanklastname').innerHTML = 'Last name cannot be blank';
		fls = 1;
	}

	if (document.leads.emailaddress.value == "") {
		document.getElementById('blankemailaddress').innerHTML = 'Please enter a valid email address';
		fls = 1;
	}


	if (checkRadio() != true && document.leads.ba.value == "0") {
		fls = 1;
	}

if (document.leads.ba.value == "0") {
	if (checkRadioB() != true && document.leads.ba.value == "0") {
		fls = 1;
	}
}

if (fls == 0) {
return true;
} else if (fls == 1) {
return false;
}
}


function checkRadio() {
	var radio_choice = false;
	for (counter = 0; counter < document.leads.staycontact.length; counter++) {
		if (document.leads.staycontact[counter].checked) {
			radio_choice = true; 
		}
	}
					
	if (!radio_choice) {
		document.getElementById('error_mail').innerHTML = "Please select 'Yes' if you would like to recieve updates from us or 'No' if you don't";
		return false;
	} else {
		return true;
	}			
}

function checkRadioB() {
	var radio_choice = false;
	for (counter = 0; counter < document.leads.attending.length; counter++) {
		if (document.leads.attending[counter].checked) {
			radio_choice = true; 
		}
	}
					
	if (!radio_choice) {
		document.getElementById('error_attending').innerHTML = "Please select 'Yes' if you are attending uni or 'No' if you're not.";
		return false;
	} else {
		return true;
	}			
}
function add_bookmark()
{
	var title = document.title;
	var url = document.location;

	if(window.sidebar) // Firefox
	{
		window.sidebar.addPanel(title, url,'');
	}
	else if(window.opera)//Opera
	{
		var a = document.createElement("A");
		a.rel = "sidebar";
		a.target = "_search";
		a.title = title;
		a.href = url;
		a.click();
	}
	else if(document.all) //IE
	{
		window.external.AddFavorite(url, title);
	}
}

var popup_on_complete = null;
var hover_box;

function create_blocking_element()
{
	window.scroll(0, 0);

	var blocking_div = document.createElement('div');
	blocking_div.setAttribute('id', 'blockingdiv');
	blocking_div.style.backgroundColor = '#000000';
	blocking_div.style.opacity = 0.50;
	blocking_div.style.filter = 'alpha(opacity=50)';
	blocking_div.style.position = 'absolute';
	document.documentElement.style.overflow = 'hidden';
	blocking_div.style.width = '100%';
	blocking_div.style.height = '100%';

	blocking_div.style.top = '0px';
	blocking_div.style.left = '0px';

	blocking_div.style.zIndex = 1001;

	document.body.appendChild(blocking_div);
}


function popup_detail(on_complete_function)
{
	var width = '48%';
	var height = '520px';

	create_blocking_sub_element();

	hover_box = document.createElement('div');
	hover_box.setAttribute('id', 'hover_box');
	hover_box.style.width = width;
	hover_box.style.height = height;
	hover_box.style.position = 'absolute';
	hover_box.style.top = '25px';
	hover_box.style.left = '26%';
	hover_box.style.border = '0 none';
	hover_box.style.backgroundColor = '#FFFFFF';

	hover_box.style.zIndex = 1002;

	var title_bar = document.createElement('table');

	var title_body = document.createElement('tbody');
	var title_row = document.createElement('tr');
	var title_cell = document.createElement('td');
	title_bar.setAttribute('id', 'hover_title');

	hover_box.appendChild(title_bar);
	title_bar.appendChild(title_body);
	title_body.appendChild(title_row);
	title_row.appendChild(title_cell);

	title_cell.setAttribute('align', 'right');
	title_cell.style.height = '20px';
	title_cell.style.width = '100%';
	title_cell.style.padding = '2px';
	title_cell.style.paddingRight = '4px';
	title_cell.innerHTML = '';
	title_bar.style.width = '100%';
	title_bar.setAttribute('cellspacing', '0');
	title_bar.setAttribute('cellpadding', '0');
	title_bar.style.borderCollapse = 'collapse';

	var content_row = document.createElement('tr');
	title_body.appendChild(content_row);
	var content_td = document.createElement('td');
	content_td.style.padding = '10px';
	content_td.innerHTML = '<h2><img src="http://www.murdoch.edu.au/_image/Icons/icon_envelope.gif/" style="vertical-align: middle; padding-right: 10px;"/>Stay in touch</h2><p>Tell us who you are and we can make sure we keep you up to date with the most helpful information. Are you a <a href="/Future-students/Domestic-students/Stay-in-touch/">domestic</a> or an <a href="/International-students/Contact-us/Make-an-enquiry/">international</a> student?</p>';
	content_row.appendChild(content_td);

	document.body.appendChild(hover_box);

	popup_on_complete = on_complete_function;
}

function popup_detail_complete()
{
	var hover_box = document.getElementById('hover_box');
	hover_box.parentNode.removeChild(hover_box);

	var blocking_el = document.getElementById('blockingdiv');
	blocking_el.parentNode.removeChild(blocking_el);

	document.documentElement.style.overflow = 'auto';
}

function popup_tisc_helppack_form()
{
	create_blocking_element();

	var hover_box = document.createElement('div');
	hover_box.id = 'hover_box';
	hover_box.style.top = '10%';
	hover_box.style.left = '10%';
	hover_box.style.position = 'absolute';
	hover_box.style.border = '0 none';
	hover_box.style.backgroundColor = '#FFFFFF';

	hover_box.style.zIndex = 1002;

	hover_box.innerHTML = '<table cellspacing="0" cellpadding="0" id="hover_title" style="width: 700px; border-collapse: collapse;"><tbody><tr><td align="right" style="padding: 2px 4px 2px 2px; background: rgb(234, 234, 235) none repeat scroll 0% 0%; height: 20px; width: 100%;"><a style="text-decoration: none; color: rgb(102, 102, 102); font-size: 11px;" onclick="popup_detail_complete();" href="#">Close</a> </td></tr><tr><td id="hover_iframe_content_td" style="height: 100%; padding: 10px;"><iframe frameborder="0" src="/TISC-Hover/?course_url=' + escape(document.location.href) + '" style="width: 100%; height: 100%;" id="tiscHoverIframe"></iframe></td></tr></tbody></table>';

	document.body.appendChild(hover_box);
}

// Resizing related functions
if( typeof addLoadEvent != "undefined" )
{
	addLoadEvent(onload_fontSize);
}


function onload_fontSize()
{
	fontSize = getCookie('fontSize');

	if( fontSize != null )
	{
		changeFontsize(parseInt(fontSize), '');
	}
}


function getCookie( name )
{
	var cname		= name + "=";

	var dc		= document.cookie;

	if( dc.length > 0 )
	{
		begin = dc.indexOf(cname);

		if( begin != -1 )
		{
			begin += cname.length;

			end = dc.indexOf(";", begin);

			if( end == -1 )
			{
				end = dc.length;
			}

			return unescape(dc.substring(begin, end));
		}
	}

	return null;
}


function setCookie( name, value, expires, path, domain, secure )
{
	document.cookie = name + "=" + escape(value) +
	((expires == null) ? "" : "; expires=" + expires.toGMTString()) +
	((path == null) ? "" : "; path=" + path) +
	((domain == null) ? "" : "; domain=" + domain) +
	((secure == null) ? "" : "; secure");
}	


function delCookie( name, path, domain )
{
	if( getCookie(name) )
	{
		document.cookie = name + "=" +
		((path == null) ? "" : "; path=" + path) +
		((domain == null) ? "" : "; domain=" + domain) +
		"; expires=Thu, 01-Jan-70 00:00:01 GMT";
	}
}


var firstCall = true;

function resizeAnchors( parent, newFontSize )
{
	anchors = parent.getElementsByTagName('a');
	for( a=0; a < anchors.length; a++ )
	{
		anchor = anchors[a];
		anchor.style.fontSize = newFontSize + "px";
	}
}

function changeFooterFontSize( fSize )
{
	max_size = 16;
	if( fSize == "" )
	{
		return;
	}
	newFontSize = parseInt(fSize);
	if( newFontSize > max_size )
	{
		newFontSize = max_size;
	}

	footer_table = null;
	if( document.getElementsByTagName )
	{
		tables = document.getElementsByTagName('table');
		for( t=0; t < tables.length; t++ )
		{
			table = tables[t];
			if( table.className == 'footer_table')
			{
				footer_table = table;
				break;
			}
		}
		if( footer_table != null )
		{
			paragraphs = footer_table.getElementsByTagName('p');
			for( p=0; p < paragraphs.length; p++ )
			{
				paragraph = paragraphs[p];
				paragraph.style.fontSize = newFontSize + "px";
				resizeAnchors(paragraph, newFontSize);
			}
		}
	}
}

function find_nav_table( classname )
{
	top_nav = null;
	if( document.getElementsByTagName )
	{
		cells = document.getElementsByTagName('td');
		for( c=0; c < cells.length; c++ )
		{
			cell = cells[c];
			if( cell.className == classname )
			{
				top_nav = cell.getElementsByTagName('table')[0];
				break;
			}
		}
	}
	return top_nav;
}

function changeMenuFontSize( fSize )
{
	if( !document.getElementsByTagName )
	{
		return;
	}

	max_size = 16;
	if( fSize == "" )
	{
		return;
	}
	
	newFontSize = parseInt(fSize);
	if( newFontSize > max_size )
	{
		newFontSize = max_size;
	}

	// Change top nav font size
	top_nav_table = find_nav_table('nt_row');
	if( top_nav_table != null )
	{
		resizeAnchors(top_nav_table, newFontSize);
	}
	
	// Change the side nav.
	side_nav_table = find_nav_table('ns_row');
	if( side_nav_table != null )
	{
		resizeAnchors(side_nav_table, newFontSize);
	}
}

function changeFontsize( fSize, increment )
{
	var p_fontsize = null;
	
	// Is this the first call?
	if( firstCall )
	{
		firstCall = false;
		if( increment != "" )
		{
			changeFontsize('11', '');
		}
	}
	
	

	if( document.getElementsByTagName )
	{
		var tags = new Array ( "h1", "h2", "h3", "p", "li" );

		for( j=0; j < tags.length; j++ )
		{
			var cell 		= document.body;

			var getElement 	= cell.getElementsByTagName(tags[j]);

			var eachElement, currentFontSize, fontIncrease, newFontSize;

			for( i=0; i<getElement.length; i++ )
			{
				eachElement = getElement[i];
				
				// Skip Module Content
				if( eachElement.parentNode.className == "module_content" || eachElement.parentNode.parentNode.className == "module_content" )
				{
					continue
				}

				// Reset the new fontsize each time.
				newFontSize = null;

				if( false == isNaN(parseInt(increment)) )
				{
					currentFontSize		= parseInt(eachElement.style.fontSize);
					fontIncrease		= parseInt(increment);
					newFontSize			= currentFontSize + fontIncrease;
				}
				else if( parseInt(fSize) > 0  )
				{
					newFontSize = parseInt(fSize);
				}
				
				if( true == isNaN(newFontSize) )
				{
					continue;
				}

				if( tags[j] == "li" )
				{
					eachElement.style.lineHeight = Math.round(newFontSize * 1.2) + "px";
				}
				else
				{
					eachElement.style.lineHeight = Math.round(newFontSize * 1.5) + "px";
				}

				if( fSize != "" )
				{
					switch( tags[j] )
					{
						case "h1":
							newFontSize += 9;
							break;

						case "h2":
							newFontSize += 6;
							break;

						case "h3":
							newFontSize += 1;
							break;

						case "h4":
							newFontSize += 0;
							break;

						case "h5":
							newFontSize += 0;
							break;

						case "h6":
							newFontSize += 0;
							break;
					}
				}

				eachElement.style.fontSize = newFontSize + "px";
				
				// Only save the fontsize if the tag element is a p. All font sizes are based of this element.
				if( (eachElement.tagName.toLowerCase() == 'p') && (parseInt(newFontSize) > 0) )
				{
					p_fontsize = newFontSize;
					setCookie('fontSize', newFontSize);
				}
			}
		}
	}
	
	
	fontsize = null;
	if( parseInt(fSize) > 0 )
	{
		fontsize = fSize;
	}
	else if( p_fontsize != null )
	{
		fontsize = p_fontsize;
	}
	
	if( fontsize != null )
	{	
		changeMenuFontSize(fontsize);
		changeFooterFontSize(fontsize);
	}
}

function create_blocking_sub_element()
{
	window.scroll(0, 0);

	var blocking_div = document.createElement('div');
	blocking_div.setAttribute('id', 'blockingdiv');
	blocking_div.style.backgroundColor = '#000000';
	blocking_div.style.opacity = 0.50;
	blocking_div.style.filter = 'alpha(opacity=50)';
	blocking_div.style.position = 'absolute';
	document.documentElement.style.overflow = 'hidden';
	blocking_div.style.width = '50%';
	blocking_div.style.height = '550px';

	blocking_div.style.top = '10px';
	blocking_div.style.left = '25%';

	blocking_div.style.zIndex = 1001;

	document.body.appendChild(blocking_div);

	var blocking_div_x = document.createElement('div');
	blocking_div_x.setAttribute('id', 'blockingdivx');
	blocking_div_x.style.left = '75%';
	blocking_div_x.style.position = 'absolute';
	blocking_div_x.style.top= '10px';
	blocking_div_x.style.width = '100px';
	blocking_div_x.style.height = '20px';
	blocking_div_x.innerHTML = '<a onclick="course_popup_detail_sub_complete();" style="cursor: pointer;"><img src="/_image/closex.gif/" border="0"></a>';
	blocking_div_x.backgroundcolor = '#ffffff';
	blocking_div_x.border = '2px solid #555555';
	blocking_div_x.padding = '3px';
	blocking_div_x.style.zIndex = 1003;
	document.body.appendChild(blocking_div_x);

}

function getY( oElement )
{
	var iReturnValue = 0;
	while( oElement != null ) {
		iReturnValue += oElement.offsetTop;
		oElement = oElement.offsetParent;
	}
	return iReturnValue;
}

function course_popup_detail(on_complete_function,unitcode)
{
	var width = '46%';
	var height = '500px';

	create_blocking_sub_element();

	hover_box = document.createElement('div');
	hover_box.setAttribute('id', 'hover_box');
	hover_box.style.width = width;
	hover_box.style.height = height;
	hover_box.style.position = 'absolute';
	hover_box.style.top = '10px';
	hover_box.style.left = '25%';
	hover_box.style.border = '0 none';
	hover_box.style.backgroundColor = '#ffffff';
	hover_box.style.margin = "15px";
	hover_box.style.padding = "10px";

	hover_box.style.zIndex = 1002;

	




	$.ajax({
			type: "POST",
			url: "/Ccpr/course_structure/unit.php?unittxt=" + unitcode,
			success: function(html){
				hover_box.innerHTML = html;
			}
		});


	
	document.body.appendChild(hover_box);

	popup_on_complete = on_complete_function;
}


function taf_popup_detail(on_complete_function)
{
	var width = '46%';
	var height = '500px';

	create_blocking_sub_element();

	hover_box = document.createElement('div');
	hover_box.setAttribute('id', 'hover_box');
	hover_box.style.width = width;
	hover_box.style.height = height;
	hover_box.style.position = 'absolute';
	hover_box.style.top = '10px';
	hover_box.style.left = '25%';
	hover_box.style.border = '0 none';
	hover_box.style.backgroundColor = '#ffffff';
	hover_box.style.margin = "15px";
	hover_box.style.padding = "10px";

	hover_box.style.zIndex = 1002;

	var title_bar = document.createElement('table');

	var title_body = document.createElement('tbody');
	var title_row = document.createElement('tr');
	var title_cell = document.createElement('td');
	title_bar.setAttribute('id', 'hover_title');

	hover_box.appendChild(title_bar);
	title_bar.appendChild(title_body);
	title_body.appendChild(title_row);
	title_row.appendChild(title_cell);

	title_cell.setAttribute('align', 'right');
	title_cell.style.height = '1px';
	title_cell.style.width = '100%';
	title_cell.style.background = '#ffffff';
	title_cell.style.padding = '2px';
	title_cell.style.paddingRight = '4px';
	title_cell.innerHTML = '';
	title_bar.style.width = '100%';
	title_bar.setAttribute('cellspacing', '0');
	title_bar.setAttribute('cellpadding', '0');
	title_bar.style.borderCollapse = 'collapse';

	var content_row = document.createElement('tr');
	title_body.appendChild(content_row);
	var content_td = document.createElement('td');
	content_td.style.padding = '10px';

	$.ajax({
			type: "POST",
			url: "/Ccpr/tell_a_friend/tellafriend.php",
			success: function(html){
				content_td.innerHTML = html;
var urlarr = document.location.href;

var urlarrb = urlarr.split("?");
var urllink = urlarrb[0];
urllink = urllink.replace("#","");
document.getElementById('url').value = urllink;
document.getElementById('link').value = urllink; 
document.getElementById('link').innerHTML = urllink;

				//var form = html.getElementById('tellafriend');
				//alert(html);

				$("#tellafriend").ajaxForm(function() {
if (validForm()) {
					$("#tellafriend_container").empty();
					$("#tellafriend_container").html("Your submission has been mailed to your friend");
} else {
return false;
}
				 });
			}
		});


	content_row.appendChild(content_td);

	document.body.appendChild(hover_box);

	popup_on_complete = on_complete_function;
}

function course_popup_detail_complete()
{
	var hover_box = document.getElementById('hover_box');
	hover_box.parentNode.removeChild(hover_box);

	var blocking_el = document.getElementById('blockingdiv');
	blocking_el.parentNode.removeChild(blocking_el);

	document.documentElement.style.overflow = 'auto';
}

function course_popup_detail_sub_complete()
{
	var hover_box = document.getElementById('hover_box');
	hover_box.parentNode.removeChild(hover_box);

	var blocking_el = document.getElementById('blockingdiv');
	blocking_el.parentNode.removeChild(blocking_el);

	var blocking_ela = document.getElementById('blockingdivx');
	blocking_ela.parentNode.removeChild(blocking_ela);

	document.documentElement.style.overflow = 'auto';
}

function taf_popup_detail_complete()
{
	var hover_box = document.getElementById('hover_box');
	hover_box.parentNode.removeChild(hover_box);

	var blocking_el = document.getElementById('blockingdiv');
	blocking_el.parentNode.removeChild(blocking_el);

	document.documentElement.style.overflow = 'auto';
}


function terms_popup_detail(on_complete_function)
{
	var width = '46%';
	var height = '500px';

	create_blocking_sub_element();

	hover_box = document.createElement('div');
	hover_box.setAttribute('id', 'hover_box');
	hover_box.style.width = width;
	hover_box.style.height = height;
	hover_box.style.position = 'absolute';
	hover_box.style.top = '10px';
	hover_box.style.left = '25%';
	hover_box.style.border = '0 none';
	hover_box.style.backgroundColor = '#ffffff';
	hover_box.style.margin = "15px";
	hover_box.style.padding = "10px";
	hover_box.style.overflow = "auto";

	hover_box.style.zIndex = 1002;

	



$.ajax({
			type: "POST",
			url: "http://www.murdoch.edu.au/Terms/",
			success: function(html){
				hover_box.innerHTML = html;
			}
		});


	
	document.body.appendChild(hover_box);

	popup_on_complete = on_complete_function;
}