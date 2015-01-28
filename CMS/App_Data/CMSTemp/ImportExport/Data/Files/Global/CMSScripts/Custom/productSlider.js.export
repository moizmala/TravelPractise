function productSlider(stepLength,width,height,container,buttonNext,buttonPrev)
{
  if ($cmsj(container+" ul li").length>stepLength)
  {
    overStep=$cmsj(container+" ul li").length%stepLength;
    blanks=0;
    if (overStep>0) blanks=stepLength-overStep;
    for (i=0;i<blanks;i++) 
      $cmsj(container+" ul").append("<li></li>");
    $cmsj(container).jCarouselLite({
      'btnNext': buttonNext,
      'btnPrev': buttonPrev,
      'width': width,
      'height': height,
      'circular': false,
      'visible': stepLength,
      'scroll': stepLength
    }); 
    $cmsj(container).show();
    $cmsj(container).mouseover(function(){
      $cmsj(buttonNext).show();
      $cmsj(buttonPrev).show();
    });
    $cmsj(container).mouseout(function(){
      $cmsj(buttonNext).hide();
      $cmsj(buttonPrev).hide();
    });
  }
}