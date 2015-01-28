/**
 * Class image selector module
 */
define(["CMS/EventHub", "jQuery"], function (hub, $) {
    "use strict";

    /**
     * Class image selector
     * @constructor
     * @param {Object} data - Data passed from the server
     */
    var ClassImageSelector = function (data) {
        var itemSelected = function () {
            var guid = $(".FlatSelectedItem img").data("metafile-guid");

            if (guid) {
                hub.publish(data.eventId, { metafileguid: guid });
            }

            CloseDialog();
        };

        // Publish guid of the selected metafile on submit
        $("#" + data.okButtonId).click(itemSelected);

        $(data.itemsCSSSelector).each(function () {
            $(this).bind('dblclick', itemSelected);
        });
    };

    return ClassImageSelector;
});