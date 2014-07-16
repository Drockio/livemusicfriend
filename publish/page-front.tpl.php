<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
	
<html xmlns="http://www.w3.org/1999/xhtml" lang="<?php print $language; ?>" 
    xml:lang="<?php print $language; ?>">

<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<title><?php print $head_title; ?></title>
    <style type="text/css" media="all">
        @import "sites/all/themes/ReWA/page-index_tpl_php.css";
    </style>
  <link type="text/css" rel="stylesheet" href="<?php print $base_path; ?>floatbox/floatbox.css" />
  <script type="text/javascript" src="<?php print $base_path; ?>floatbox/floatbox.js"></script>
  <?php print $head; ?>
  <?php print $styles; ?>

  <!--[if IE 7]>
    <link rel="stylesheet" href="<?php print $base_path . $directory .'/ie/ie_7.css'; ?>" rel="stylesheet" type="text/css" />
  <![endif]-->
  <!--[if IE 6]>
    <link rel="stylesheet" href="<?php print $base_path . $subtheme_directory .'/ie/ie_6.css'; ?>" rel="stylesheet" type="text/css" />
	<![endif]-->
	
  <?php print $scripts; ?>





<script language="JavaScript" type="text/javascript">
<!--
//v1.7
// Flash Player Version Detection
// Detect Client Browser type
// Copyright 2005-2008 Adobe Systems Incorporated.  All rights reserved.
var isIE  = (navigator.appVersion.indexOf("MSIE") != -1) ? true : false;
var isWin = (navigator.appVersion.toLowerCase().indexOf("win") != -1) ? true : false;
var isOpera = (navigator.userAgent.indexOf("Opera") != -1) ? true : false;
function ControlVersion()
{
	var version;
	var axo;
	var e;
	// NOTE : new ActiveXObject(strFoo) throws an exception if strFoo isn't in the registry
	try {
		// version will be set for 7.X or greater players
		axo = new ActiveXObject("ShockwaveFlash.ShockwaveFlash.7");
		version = axo.GetVariable("$version");
	} catch (e) {
	}
	if (!version)
	{
		try {
			// version will be set for 6.X players only
			axo = new ActiveXObject("ShockwaveFlash.ShockwaveFlash.6");
			
			// installed player is some revision of 6.0
			// GetVariable("$version") crashes for versions 6.0.22 through 6.0.29,
			// so we have to be careful. 
			
			// default to the first public version
			version = "WIN 6,0,21,0";
			// throws if AllowScripAccess does not exist (introduced in 6.0r47)		
			axo.AllowScriptAccess = "always";
			// safe to call for 6.0r47 or greater
			version = axo.GetVariable("$version");
		} catch (e) {
		}
	}
	if (!version)
	{
		try {
			// version will be set for 4.X or 5.X player
			axo = new ActiveXObject("ShockwaveFlash.ShockwaveFlash.3");
			version = axo.GetVariable("$version");
		} catch (e) {
		}
	}
	if (!version)
	{
		try {
			// version will be set for 3.X player
			axo = new ActiveXObject("ShockwaveFlash.ShockwaveFlash.3");
			version = "WIN 3,0,18,0";
		} catch (e) {
		}
	}
	if (!version)
	{
		try {
			// version will be set for 2.X player
			axo = new ActiveXObject("ShockwaveFlash.ShockwaveFlash");
			version = "WIN 2,0,0,11";
		} catch (e) {
			version = -1;
		}
	}
	
	return version;
}
// JavaScript helper required to detect Flash Player PlugIn version information
function GetSwfVer(){
	// NS/Opera version >= 3 check for Flash plugin in plugin array
	var flashVer = -1;
	
	if (navigator.plugins != null && navigator.plugins.length > 0) {
		if (navigator.plugins["Shockwave Flash 2.0"] || navigator.plugins["Shockwave Flash"]) {
			var swVer2 = navigator.plugins["Shockwave Flash 2.0"] ? " 2.0" : "";
			var flashDescription = navigator.plugins["Shockwave Flash" + swVer2].description;
			var descArray = flashDescription.split(" ");
			var tempArrayMajor = descArray[2].split(".");			
			var versionMajor = tempArrayMajor[0];
			var versionMinor = tempArrayMajor[1];
			var versionRevision = descArray[3];
			if (versionRevision == "") {
				versionRevision = descArray[4];
			}
			if (versionRevision[0] == "d") {
				versionRevision = versionRevision.substring(1);
			} else if (versionRevision[0] == "r") {
				versionRevision = versionRevision.substring(1);
				if (versionRevision.indexOf("d") > 0) {
					versionRevision = versionRevision.substring(0, versionRevision.indexOf("d"));
				}
			}
			var flashVer = versionMajor + "." + versionMinor + "." + versionRevision;
		}
	}
	// MSN/WebTV 2.6 supports Flash 4
	else if (navigator.userAgent.toLowerCase().indexOf("webtv/2.6") != -1) flashVer = 4;
	// WebTV 2.5 supports Flash 3
	else if (navigator.userAgent.toLowerCase().indexOf("webtv/2.5") != -1) flashVer = 3;
	// older WebTV supports Flash 2
	else if (navigator.userAgent.toLowerCase().indexOf("webtv") != -1) flashVer = 2;
	else if ( isIE && isWin && !isOpera ) {
		flashVer = ControlVersion();
	}	
	return flashVer;
}
// When called with reqMajorVer, reqMinorVer, reqRevision returns true if that version or greater is available
function DetectFlashVer(reqMajorVer, reqMinorVer, reqRevision)
{
	versionStr = GetSwfVer();
	if (versionStr == -1 ) {
		return false;
	} else if (versionStr != 0) {
		if(isIE && isWin && !isOpera) {
			// Given "WIN 2,0,0,11"
			tempArray         = versionStr.split(" "); 	// ["WIN", "2,0,0,11"]
			tempString        = tempArray[1];			// "2,0,0,11"
			versionArray      = tempString.split(",");	// ['2', '0', '0', '11']
		} else {
			versionArray      = versionStr.split(".");
		}
		var versionMajor      = versionArray[0];
		var versionMinor      = versionArray[1];
		var versionRevision   = versionArray[2];
        	// is the major.revision >= requested major.revision AND the minor version >= requested minor
		if (versionMajor > parseFloat(reqMajorVer)) {
			return true;
		} else if (versionMajor == parseFloat(reqMajorVer)) {
			if (versionMinor > parseFloat(reqMinorVer))
				return true;
			else if (versionMinor == parseFloat(reqMinorVer)) {
				if (versionRevision >= parseFloat(reqRevision))
					return true;
			}
		}
		return false;
	}
}
function AC_AddExtension(src, ext)
{
  if (src.indexOf('?') != -1)
    return src.replace(/\?/, ext+'?'); 
  else
    return src + ext;
}
function AC_Generateobj(objAttrs, params, embedAttrs) 
{ 
  var str = '';
  if (isIE && isWin && !isOpera)
  {
    str += '<object ';
    for (var i in objAttrs)
    {
      str += i + '="' + objAttrs[i] + '" ';
    }
    str += '>';
    for (var i in params)
    {
      str += '<param name="' + i + '" value="' + params[i] + '" /> ';
    }
    str += '</object>';
  }
  else
  {
    str += '<embed ';
    for (var i in embedAttrs)
    {
      str += i + '="' + embedAttrs[i] + '" ';
    }
    str += '> </embed>';
  }
  document.write(str);
}
function AC_FL_RunContent(){
  var ret = 
    AC_GetArgs
    (  arguments, ".swf", "movie", "clsid:d27cdb6e-ae6d-11cf-96b8-444553540000"
     , "application/x-shockwave-flash"
    );
  AC_Generateobj(ret.objAttrs, ret.params, ret.embedAttrs);
}
function AC_SW_RunContent(){
  var ret = 
    AC_GetArgs
    (  arguments, ".dcr", "src", "clsid:166B1BCA-3F9C-11CF-8075-444553540000"
     , null
    );
  AC_Generateobj(ret.objAttrs, ret.params, ret.embedAttrs);
}
function AC_GetArgs(args, ext, srcParamName, classid, mimeType){
  var ret = new Object();
  ret.embedAttrs = new Object();
  ret.params = new Object();
  ret.objAttrs = new Object();
  for (var i=0; i < args.length; i=i+2){
    var currArg = args[i].toLowerCase();    
    switch (currArg){	
      case "classid":
        break;
      case "pluginspage":
        ret.embedAttrs[args[i]] = args[i+1];
        break;
      case "src":
      case "movie":	
        args[i+1] = AC_AddExtension(args[i+1], ext);
        ret.embedAttrs["src"] = args[i+1];
        ret.params[srcParamName] = args[i+1];
        break;
      case "onafterupdate":
      case "onbeforeupdate":
      case "onblur":
      case "oncellchange":
      case "onclick":
      case "ondblclick":
      case "ondrag":
      case "ondragend":
      case "ondragenter":
      case "ondragleave":
      case "ondragover":
      case "ondrop":
      case "onfinish":
      case "onfocus":
      case "onhelp":
      case "onmousedown":
      case "onmouseup":
      case "onmouseover":
      case "onmousemove":
      case "onmouseout":
      case "onkeypress":
      case "onkeydown":
      case "onkeyup":
      case "onload":
      case "onlosecapture":
      case "onpropertychange":
      case "onreadystatechange":
      case "onrowsdelete":
      case "onrowenter":
      case "onrowexit":
      case "onrowsinserted":
      case "onstart":
      case "onscroll":
      case "onbeforeeditfocus":
      case "onactivate":
      case "onbeforedeactivate":
      case "ondeactivate":
      case "type":
      case "codebase":
      case "id":
        ret.objAttrs[args[i]] = args[i+1];
        break;
      case "width":
      case "height":
      case "align":
      case "vspace": 
      case "hspace":
      case "class":
      case "title":
      case "accesskey":
      case "name":
      case "tabindex":
        ret.embedAttrs[args[i]] = ret.objAttrs[args[i]] = args[i+1];
        break;
      default:
        ret.embedAttrs[args[i]] = ret.params[args[i]] = args[i+1];
    }
  }
  ret.objAttrs["classid"] = classid;
  if (mimeType) ret.embedAttrs["type"] = mimeType;
  return ret;
}
// -->
</script>
</head>
<body>

	<div id="page_wrapper">
		<div id="page" class="<?php if ($right) {print "two_column"; }?>">
		
			<div id="top">
			
			<?php if ($top_nav): ?>
	<div id="top_nav">
				<?php print $top_nav; ?>
</div><!-- /top_nav-->
			<?php endif; ?>
			<?php print $search_box ?>
		
				</div><!-- /top -->
			
				<div id="header">

					<div id="logo" >
						<?php if ($logo): ?>
							<a href="<?php print $base_path; ?>" title="<?php print t('Home'); ?>">
								<img src="<?php print $logo; ?>" alt="<?php print t('Home'); ?>" />
</a>
						<?php endif; ?>
					</div><!-- /logo -->
					<div id="text">
						<h1>ReWA</h1>
						<h2>Empowering families, strengthening communities</h2>
					</div>
					<div id="donate">
						<img src="files/page_images/bg_arc.jpg"/>
						<h2 id="donate_h2"><a href="https://rewa.ejoinme.org/MyPages/DonationPage/tabid/72967/Default.aspx">DONATE</a></h2>
					</div>
				</div><!-- /header -->
			
	
			<div id="primary_nav_bar">
				<?php print $header; ?>
			</div>
	
	
			<div id="content">	
				<div id="main">	
					<div id="left_content">
					<div id="movie" style="text-align: center;">
                            <h1 style="letter-spacing: -2px; font-weight: bold; text-align: center; font-size: 40px; margin-bottom: 0px; padding-bottom: 0px; color:#562b7c; ">The ReWA Effect</h1>
                            <!--Movie-->
                            <embed src="http://rewa.org/files/the-rewa-effect.mp4" width="600" height="354" scale="aspect" controller="true" style="border: 9px solid #e9eaee;">
                            <p style="text-align: center; font-weight: bolder; margin-bottom: 40px;">We come from differenct places, but share one story</p>
                        </div>
						<!--<div class="slideshow">

<script language="JavaScript" type="text/javascript">
	AC_FL_RunContent(
		'codebase', 'http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,0,0',
		'width', '690',
		'height', '310',
		'src', 'slideshow',
		'quality', 'high',
		'pluginspage', 'http://www.adobe.com/go/getflashplayer',
		'align', 'middle',
		'play', 'true',
		'loop', 'true',
		'scale', 'showall',
		'wmode', 'transparent',
		'devicefont', 'false',
		'id', 'slideshow',
		'bgcolor', '#ffffff',
		'name', 'slideshow',
		'menu', 'true',
		'allowFullScreen', 'false',
		'allowScriptAccess','sameDomain',
		'movie', 'slideshow',
		'salign', ''
		); //end AC code
</script>
<noscript>
	<object classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,0,0" width="690" height="310" id="slideshow" align="middle">
	<param name="allowScriptAccess" value="sameDomain" />
	<param name="allowFullScreen" value="false" />
	<param name="movie" value="files/slideshow.swf" /><param name="quality" value="high" /><param name="bgcolor" value="#ffffff" />	<embed src="files/slideshow.swf" quality="high" bgcolor="#ffffff" width="690" height="310" name="slideshow" align="middle" allowScriptAccess="sameDomain" allowFullScreen="false" type="application/x-shockwave-flash" pluginspage="http://www.adobe.com/go/getflashplayer" />
	</object>
</noscript>



						</div>-->
						<div class="mission_statement">
							<p>ReWA is a non-profit, multi-ethnic organization that promotes inclusion, independence, personal leadership, and strong communities by providing refugee and immigrant women and their families with culturally and linguistically appropriate services. ReWA advocates for social justice, public policy changes, and equal access to services while respecting cultural values and the right to self-determination.</p>
						</div>
						<div class="program_highlights">
							<div class="headers">
								<h3 style="color: white;">Program Highlights</h3>
								<h3>Outcomes</h3>
								<h3>Your Investment</h3>
							</div>
							<div class="three_photos">
								<a href="?q=node/9"><img src="files/page_images/home_program_highlight.jpg" alt="Explore ReWA's Early Childhood Education Program" title="Explore ReWA's Early Childhood Education Program"/></a>
								<a href="?q=node/21"><img src="files/page_images/home_outcomes.jpg" alt="View the effects of ReWA's programs on the community" title="View the effects of ReWA's programs on the community"/></a>
								<a href="?q=node/26"><img src="files/page_images/home_your_investment.jpg" alt="See how your contributions help refugees and immigrants" title="See how your contributions help refugees and immigrants"/></a>
							</div>
						</div>
						
					</div><!-- /itemWrapper -->
							
						<?php if ($right): ?>
	
	<div id="sidebar_right">
							<?php if ($secondary_links): ?>
	<h2 class="title">Categories</h2>
	<div id="secondary">
	
								<?php print theme('menu_links', $secondary_links); ?>
<br style="clear:both;" />
</div>
								<?php endif; ?>

								<?php print $right; ?>
</div><!-- /right -->

						<?php endif; ?>

												
					
				</div><!-- /main -->
			</div><!-- /content -->
	
	
			<div id="footer">
				<div id="social">
					<span>FIND US</span>
					<div id="icons">
						<a href="http://www.facebook.com/pages/Refugee-Womens-Alliance/104322283885" title="ReWA Facebook link" target="_blank"><img src="files/page_images/bg_social_facebook.png" /></a>
						<a href="http://www.youtube.com/user/rewaAdmin" title="ReWA YouTube link" target="_blank"><img src="files/page_images/bg_social_youtube.jpg" /></a><br/>
						<!--<a href="files/resources/ReWA_song.mp3" target="_blank" title="ReWA Song" ><img src="files/page_images/music_note.png"/></a><br/>
							<spa\n>ReWA song</span>-->
					</div>
					<img src="files/page_images/unitedway.jpg"/>
					<p>Sign up for our E-News and Newsletters <a href="?q=node/41">here</a></p>
				</div>
	
				<?php print $footer_message ?>

			</div><!-- /footer -->
		
			
		
		</div><!-- /page -->
	</div><!-- /page_wrapper -->
<?php print $closure ?>

</body>
</html>

