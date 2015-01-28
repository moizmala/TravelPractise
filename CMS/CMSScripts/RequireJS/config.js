require.config({
    baseUrl: '{%AppPath%}/CMSPages/GetResource.ashx?scriptmodule=',
    paths: {
        'jQuery': '{%AppPath%}/CMSScripts/jQuery/jquery-core',
        'jQueryUI': '{%AppPath%}/CMSScripts/Vendor/jQueryUI/jquery-ui-1.10.4.min',
        'Underscore': '{%AppPath%}/CMSScripts/underscore/underscore.min',
        'jQueryJScrollPane': '{%AppPath%}/CMSScripts/jquery-jscrollpane',
        'jQueryFancySelector': '{%AppPath%}/CMSScripts/jquery/jquery-fancyselect',

        'angular': '{%AppPath%}/CMSScripts/Vendor/Angular/angular',
        'angular-resource': '{%AppPath%}/CMSScripts/Vendor/Angular/angular-resource.min',
        'angular-route': '{%AppPath%}/CMSScripts/Vendor/Angular/angular-route.min',
        'angular-animate': '{%AppPath%}/CMSScripts/Vendor/Angular/angular-animate',
        'angularSortable': '{%AppPath%}/CMSScripts/Vendor/Angular/angular-sortable',
        'lodash': '{%AppPath%}/CMSScripts/Vendor/LoDash/lodash.min'
    },
    shim: {
        'jQuery': { exports: '$cmsj' },
        'jQueryUI': { deps: ['jQuery'] },
        'Underscore': { exports: '_' },
        'jQueryJScrollPane': { deps: ['jQuery'] },
        'jQueryFancySelector': { deps: ['jQuery'] },

        'angular': { exports: 'angular' },
        'angular-resource': { deps: ['angular'] },
        'angular-route': { deps: ['angular'] },
        'angular-animate': { deps: ['angular'] },
        'lodash': { exports: '_' },

        'angularSortable': { deps: ['jQuery', 'jQueryUI', 'angular'] }
    },

    priority: [
        'jQuery',
        'jQueryUI',
        'angular',
        'angular-route',
        'angular-animate',
        'angularSortable'
    ]
});