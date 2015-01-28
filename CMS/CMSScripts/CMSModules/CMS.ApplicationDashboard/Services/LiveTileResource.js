define(["require", "exports", 'CMS/Application'], function(require, exports, application) {
    exports.Resource = [
        '$resource',
        function ($resource) {
            return $resource(application.getData('applicationUrl') + 'cmsapi/LiveTile/', {}, {});
        }
    ];
});
