
(function () {
    var activeTooltip = null;
    var activeArrow = null;

    function closeTooltip() {
        if (activeTooltip) {
            activeTooltip.classList.remove('bm-tooltip-visible');
            activeTooltip = null;
        }
        if (activeArrow) {
            activeArrow.remove();
            activeArrow = null;
        }
    }

    document.addEventListener('click', function (e) {
        var trigger = e.target.closest('[data-bm-tooltip]');

        if (!trigger) {
            closeTooltip();
            return;
        }

        var id = trigger.getAttribute('data-bm-tooltip');
        var tooltip = document.getElementById(id);
        if (!tooltip) return;

        // If clicking same trigger, toggle off
        if (activeTooltip === tooltip) {
            closeTooltip();
            return;
        }

        closeTooltip();

        var rect = trigger.getBoundingClientRect();
        var spaceAbove = rect.top;
        var spaceBelow = window.innerHeight - rect.bottom;
        var placeAbove = spaceAbove > 150 || spaceAbove > spaceBelow;

        // Move to body first so we can measure correctly
        if (tooltip.parentElement !== document.body) {
            document.body.appendChild(tooltip);
        }

        tooltip.classList.add('bm-tooltip-visible');

        // Measure after visible and in body
        var tw = tooltip.offsetWidth;
        var th = tooltip.offsetHeight;
        var left = rect.left + rect.width / 2 - tw / 2;
        left = Math.max(8, Math.min(left, window.innerWidth - tw - 8));

        var arrow = document.createElement('div');
        arrow.className = 'bm-tooltip-arrow';

        if (placeAbove) {
            tooltip.style.top = (rect.top - th - 10) + 'px';
            tooltip.style.left = left + 'px';
            arrow.classList.add('bm-tooltip-arrow--up');
            arrow.style.top = (rect.top - 10) + 'px';
            arrow.style.left = (rect.left + rect.width / 2 - 6) + 'px';
        } else {
            tooltip.style.top = (rect.bottom + 10) + 'px';
            tooltip.style.left = left + 'px';
            arrow.classList.add('bm-tooltip-arrow--down');
            arrow.style.top = (rect.bottom + 4) + 'px';
            arrow.style.left = (rect.left + rect.width / 2 - 6) + 'px';
        }

        document.body.appendChild(arrow);
        activeTooltip = tooltip;
        activeArrow = arrow;
        e.stopPropagation();
    });
})();