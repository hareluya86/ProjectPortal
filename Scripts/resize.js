if( typeof init_functions != "undefined" )
{
	init_functions.push("onload_fontSize()");
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

function findTopNav()
{
	top_nav = null;
	if( document.getElementsByTagName )
	{
		cells = document.getElementsByTagName('td');
		for( c=0; c < cells.length; c++ )
		{
			cell = cells[c];
			if( cell.className == 'nt_row' )
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

	// change top nav font size
	top_nav_table = findTopNav();
	if( document.getElementsByTagName )
	{
		if( top_nav_table != null )
		{
			resizeAnchors(top_nav_table, newFontSize);
		}
	}
}

function changeFontsize( fSize, increment )
{
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
		tags = new Array ( "h1", "h2", "h3", "p", "li" );

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

				if( increment != "" )
				{
					currentFontSize		= parseInt(eachElement.style.fontSize);
					fontIncrease		= parseInt(increment);
					newFontSize			= currentFontSize + fontIncrease;
				}
				else if( fSize != "" )
				{
					newFontSize = parseInt(fSize);
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
							newFontSize += 5;
							break;

						case "h3":
							newFontSize += 0;
							break;

						case "h4":
							newFontSize += 0;
							break;

						case "h5":
							newFontSize += 0;
							break;

						case "h6":
							newFontSize += 0;
					}
				}

				eachElement.style.fontSize = newFontSize + "px";

				setCookie('fontSize', newFontSize);
			}
		}
	}
	changeMenuFontSize(fSize);
	changeFooterFontSize(fSize);
}