
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

$(document).on('show.bs.collapse', function(e) {
    var btn = $('[data-target="#' + e.target.id + '"]');
    btn.find('polyline').attr('points', '18 15 12 9 6 15');
    $('#' + e.target.id.replace('-content', '-toggle')).val('1');
});

$(document).on('hide.bs.collapse', function(e) {
    var btn = $('[data-target="#' + e.target.id + '"]');
    btn.find('polyline').attr('points', '6 9 12 15 18 9');
    $('#' + e.target.id.replace('-content', '-toggle')).val('0');
});

$(document).on('change', '.game-date-toggle-checkbox', function() {
    var row = $(this).closest('label').parent();
    if ($(this).is(':checked')) {
        row.addClass('game-date-row--selected');
    } else {
        row.removeClass('game-date-row--selected');
    }
});

// Popup Calendar - toggle open/close
document.addEventListener('click', function(e) {
    var trigger = e.target.closest('[data-popup-cal]');

    if (trigger) {
        var panelId = trigger.getAttribute('data-popup-cal');
        var panel = document.getElementById(panelId);
        if (!panel) return;
        var isOpen = panel.classList.contains('popup-cal-open');
        document.querySelectorAll('.popup-cal-panel.popup-cal-open')
            .forEach(function(p) { p.classList.remove('popup-cal-open'); });
        if (!isOpen) panel.classList.add('popup-cal-open');
        e.stopPropagation();
        return;
    }

    // Click outside closes all
    if (!e.target.closest('.popup-cal-panel')) {
        document.querySelectorAll('.popup-cal-panel.popup-cal-open')
            .forEach(function(p) { p.classList.remove('popup-cal-open'); });
    }
});

// Popup Calendar - day selection
document.addEventListener('click', function(e) {
    var day = e.target.closest('[data-popup-cal-date]');
    if (!day) return;

    var dateVal = day.getAttribute('data-popup-cal-date');
    var panel = day.closest('.popup-cal-panel');
    if (!panel) return;

    // Set hidden input
    var wrapperId = panel.id.replace('-panel', '');
    var hidden = document.getElementById(wrapperId + '-selected');
    if (hidden) hidden.value = dateVal;

    // Close panel
    panel.classList.remove('popup-cal-open');

    // Submit parent form
    var form = panel.closest('form');
    if (form) form.submit();
});
