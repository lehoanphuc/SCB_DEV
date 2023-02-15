var Portal = Class.create();
Portal.prototype = {
    initialize: function (options) {

        this.options = {
            portalId: 'portal',
            columnClass: 'portal-column',
            blockClass: 'block',
            contentClass: 'content',
            handleClass: 'handle',
            hoverClass: 'block-hover',
            toggleClass: 'block-toggle',
            blocklistId: 'portal-column-block-list',
            blocklistlinkId: 'portal-block-list-link',
            blocklisthandleClass: 'block-list-handle',
            watch_textareaId: 'watch',
            saveurl: ''
        }
        Object.extend(this.options, options || {});

        this.columns = { el: $$('#' + this.options.portalId + ' .' + this.options.columnClass) };
        this.blocks = { el: $$('#' + this.options.portalId + ' .' + this.options.blockClass) };

        this.columns.el.each(function (column) {

            Sortable.create(column, {
                containment: this.columns.el,
                constraint: false,
                tag: 'div',
                only: this.options.blockClass,
                dropOnEmpty: true,
                handle: this.options.handleClass,
                hoverclass: this.options.hoverClass,
                onUpdate: function (column) {

                    if (!this.options.saveurl) {
                        return;
                    }
                    if (column.id == this.options.blocklistId) {
                        return;
                    }
                    var url = this.options.saveurl;
                    var postBody = column.id + ':';
                    postBody += this.blocks.el.pluck('id').join(',');
                    postBody = 'value=' + escape(postBody);
                    if ($(this.options.watch_textareaId)) { $(this.options.watch_textareaId).value += postBody + "\n" }

                    new Ajax.Request(url, {
                        method: 'post',
                        postBody: postBody
                    }
                    );
                }.bind(this)
            });
        }.bind(this));

        $A(this.blocks.el).each(
          function (block) {
              var toggle = block.getElementsByClassName(this.options.toggleClass)[0];

              toggle.observe(
                'click',
                function (e) {
                    el = e.element();
                    el.ancestors().collect(
                      function (e) {
                          if (e.hasClassName(this.options.blockClass)) {
                              parentBlock = e;
                              throw $break;
                          } else {
                              return false;
                          }
                      }
                    );

                    content = parentBlock.getElementsByClassName(this.options.contentClass)[0];
                    Effect.toggle($(content), 'Slide');
                }.bindAsEventListener(this)
              )

          }.bind(this)
        );
    },

    displayBlockList: function (e) {

        Effect.toggle(this.options.blocklistId);
        Event.stop(e);
    },

    applySettings: function (settings) {

        for (var container in settings) {
            settings[container].each(function (block) {
                $(container).appendChild($(block));
            });
        }
    }
}
