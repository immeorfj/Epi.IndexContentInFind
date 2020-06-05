define([
        "dojo/_base/declare",
        "geta-epi-indexcontentinfind/command/_IndexContentInFindCommandMixin",
        "epi/i18n!epi/cms/nls/geta.epi.indexcontentinfind.indexcontentinfindcommand",
        "epi/shell/_ContextMixin"
],
    function(
        declare,
        _IndexContentInFindCommandMixin,
        resources,
        context
    ) {
        return declare("geta-epi-indexcontentinfind/command/IndexContentInFindCommand", [_IndexContentInFindCommandMixin], {
            name: "IndexContentInFindCommand",
            label: resources.label,
            tooltip: resources.tooltip,
            includeDescendants: false,
            resources: resources
        });
    });