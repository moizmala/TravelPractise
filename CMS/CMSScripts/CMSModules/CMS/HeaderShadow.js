define(['jQuery', 'Underscore', 'CMS/EventHub'], function ($, _, eventHub) {

    var $uiHeader,
        $shadowSet,

        doc = $(document),  

        toggleShadow = _.throttle(function () {
            ensureUiHeader();

            if ($uiHeader.length > 0) {
                toggleShadowInternal(this);
            }
        }, 1000 / 60), // 30 FPS is good enough

        ensureUiHeader = function () {
            if ($uiHeader == null || $uiHeader.length <= 0) {
                // Try to find explicit shadow holder panel
                $uiHeader = $('div.shadow-holder');
                checkEmptyness();
                // Try to find other header panels
                if ($uiHeader.length <= 0) {
                    $uiHeader = $('div.PreviewMenu');
                }
                if ($uiHeader.length <= 0) {
                    $uiHeader = $('div#CMSHeaderDiv');
                    checkEmptyness();
                }
                if ($uiHeader.length <= 0) {
                    $uiHeader = $('div.header-container');
                    checkEmptyness();
                }
                if ($uiHeader.length <= 0) {
                    $uiHeader = $('div#CKToolbar');
                    checkEmptyness();
                }
                if ($uiHeader.length <= 0) {
                    $uiHeader = $('div.cms-edit-menu');
                }
            }
        },

        checkEmptyness = function () {
            if (($uiHeader.children().length <= 0) || ($uiHeader.height() == 0)) {
                $uiHeader = [];
            }
        },

        toggleShadowInternal = function (scrollElem) {
            if ($shadowSet === false) {
                if ($(scrollElem).scrollTop() > 0) {
                    $uiHeader.first().addClass('header-shadow');
                    $shadowSet = true;
                }
            }

            if ($shadowSet === true) {
                if ($(scrollElem).scrollTop() <= 0) {
                    $uiHeader.first().removeClass('header-shadow');
                    $shadowSet = false;
                }
            }
        },

        initializeShadow = function() {
            doc.on('scroll', toggleShadow);
            $('.scroll-area,.PageContent,.PreviewBody').on('scroll', toggleShadow);

            $('iframe.scroll-area').on('load', function() {
                 $(this).contents().on('scroll', toggleShadow);
            });
            
            $uiHeader = [];

            if ($shadowSet == null) {
                $shadowSet = false;
            }

            // Reset shadow after async postback
            if ($shadowSet == true) {
                ensureUiHeader();
                if (($uiHeader != null) && ($uiHeader.length > 0)) {
                    $uiHeader.first().addClass('header-shadow');
                }
            }
        };

    doc.ready(initializeShadow);
    eventHub.subscribe('cms.viewLoaded', initializeShadow);

    return function() {}
});