(function (window, document, Number, Math) {

    function TwoHeadedScrollable() {
        this.classNameIdentifyer = 'MainScrollableContainer';
    }

    TwoHeadedScrollable.prototype.resolve = function () {
        var scrollables = document.getElementsByClassName(this.classNameIdentifyer);

        Array.prototype.forEach.call(scrollables, function (scrollable, index) {
            if (!scrollable.twoHeadedScrollable) {
                scrollable.twoHeadedScrollable = new Controller(scrollable);
            } else {
                console.log('----------------------------------');
                console.log(scrollable);
                console.log('is already resolved');
            }
        })
    }

    function Controller(context) {
        this.context = context;
        this.topHeadline = this.context.getElementsByClassName('top-headline')[0];
        this.scrollContainer = this.context.getElementsByClassName('scroll-container')[0];

        this.prepareSubHeadlineData();
        this.subHeadlineContainer = this.context.getElementsByClassName('second-headline-container')[0];
        
        this.observe();
    }

    Controller.prototype.prepareSubHeadlineData = function () {
        var controller = this;
        var subHeadlineElements = controller.context.getElementsByClassName('sub-headline');
        controller.subHeadlines = [];

        Array.prototype.forEach.call(subHeadlineElements, function (subHeadlineElement, index) {
            subHeadlineElement.index = index;
            controller.subHeadlines.push({
                element: subHeadlineElement
            })
        });
    }
    
    Controller.prototype.observe = function () {
        var self = this;

        var minScrollTime = 100;
        var scrollTimer, lastScrollFireTime = 0;

        function processScroll() {
            self.updateSubHeadlineStickers();
        }

        this.scrollContainer.addEventListener('scroll', function () {
            var now = new Date().getTime();

            if (!scrollTimer) {
                if (now - lastScrollFireTime > (3 * minScrollTime)) {
                    processScroll();   // fire immediately on first scroll
                    lastScrollFireTime = now;
                }
                scrollTimer = setTimeout(function () {
                    scrollTimer = null;
                    lastScrollFireTime = new Date().getTime();
                    processScroll();
                }, minScrollTime);
            }
        });
    }

    Controller.prototype.updateSubHeadlineStickers = function () {
        var containerPosition = this.scrollContainer.getBoundingClientRect().top;
       
        var lastPos = -1;
        var distance = 0;
        for (var i = 0; i < this.subHeadlines.length; i++) {
            var current = this.subHeadlines[i];
            var currentHeadlinePos = current.element.getBoundingClientRect().top;
            var distanceToTopOfScrollContainer = currentHeadlinePos - containerPosition;
            if(distanceToTopOfScrollContainer <= 0) {
                lastPos = i;
                distance = distanceToTopOfScrollContainer;
            }
        }
        if (lastPos > -1)
            this.updateSubHeadlineSticker(this.subHeadlines[lastPos], lastPos == 0, distance, this.subHeadlineContainer);
    }
    
    function MoveInnerElements(oldParent, newParent, text) {
        $(oldParent).children().detach().appendTo($(newParent));
    }

    Controller.prototype.updateSubHeadlineSticker = function (subHeadline, isFirstElement, distanceToTopOfScrollContainer, secondHeadlineContainer) {

        var isTheActiveElement = subHeadline.element.classList.contains('active');

        if (distanceToTopOfScrollContainer < 0) {

            if (isTheActiveElement) return;
            // hol dir den headline container 
            // was dort drin steht in den active container schreiben             
            var activeHeadline = this.scrollContainer.getElementsByClassName('active')[0];
            if (activeHeadline) { // activeHeadlines are always empty !
                MoveInnerElements(secondHeadlineContainer, activeHeadline, "cleanActive");
                activeHeadline.classList.remove("active");
            }
            subHeadline.element.classList.add("active");
            MoveInnerElements(subHeadline.element, secondHeadlineContainer, "set Content");
        }
        else if (distanceToTopOfScrollContainer == 0) {
            if (!isFirstElement || !isTheActiveElement) return;
            subHeadline.element.classList.remove('active');
            MoveInnerElements(secondHeadlineContainer, subHeadline.element, "whatever");
        }
        return;       
    }

    window.TwoHeadedScrollable = new TwoHeadedScrollable();
})(window, document, Number, Math);

TwoHeadedScrollable.resolve();
