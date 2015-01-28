define(["require", "exports", 'angular', 'CMS.ApplicationDashboard/Directives.TileDirective', 'CMS.ApplicationDashboard/Directives.WelcomeTileDirective'], function(require, exports, angular, tileDirective, welcomeTileDirective) {
    var ModuleName = 'cms.dashboard.directives';

    angular.module(ModuleName, []).directive('tile', tileDirective.Directive).directive('welcometile', welcomeTileDirective.Directive);

    exports.Module = ModuleName;
});
