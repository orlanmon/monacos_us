<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"  xmlns:fn="http:=//www.w3.org/2005/xpath-functions" >

<xsl:output method="html" indent="yes"/>
<xsl:param name="RSS_Feed_Title"></xsl:param>
  
  <xsl:template match="/">
  

    <div>
      <div class="rssfeed_content_area" >

        <div class="rssfeed_content">
        <a href="{/rss/channel/link}" class="rssfeed_Link"  target="_blank">

          <img src="./images/rss.png" >

            <xsl:attribute name="title">
              <xsl:value-of select="/rss/channel/title" />
            </xsl:attribute>
            
          </img>
          <div id="lbl_RSSFeedTitle" class="rssfeed_header">
            <xsl:value-of select="$RSS_Feed_Title" /> 
          </div>
        </a>
        <div class="rssfeed_headerline"></div>
          <div class="rssfeed_scrolling" >
         
              <xsl:apply-templates select="/rss/channel/item"/>
         
          </div>
        </div>
      </div>
    </div>
  </xsl:template>

  <xsl:template match="item">
    
      <a href="{link}" class="rssfeed_Link" target="_blank">
        <xsl:value-of select="title"/>

        <xsl:attribute name="title">
          <xsl:value-of select="'Select to Preview.'" />
        </xsl:attribute>
        
        
      </a>
      <br/>
      <div class="rssfeed_text_smaller">
        <xsl:value-of select='substring( pubDate, 0,17)' />
      </div>
      <div class="rssfeed_line"></div>
    
    
  </xsl:template>
</xsl:stylesheet>
