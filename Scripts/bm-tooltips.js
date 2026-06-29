
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

    e.stopPropagation();

    if (day.classList.contains('popup-cal-day--disabled')) return;

    var dateVal = day.getAttribute('data-popup-cal-date');
    var panel = day.closest('.popup-cal-panel');
    if (!panel) return;
    var wrapperId = panel.id.replace('-panel', '');
    var wrapper = document.getElementById(wrapperId);
    if (wrapper && wrapper.getAttribute('data-mode') === 'range') return; 

    // Update hidden field
    var hidden = document.getElementById(wrapperId + '-selected');
    if (hidden) hidden.value = dateVal;

    // Update trigger button text and icon
    var trigger = document.querySelector('[data-popup-cal="' + panel.id + '"]');
    if (trigger) {
        var date = new Date(dateVal + 'T00:00:00');
        var formatted = date.toLocaleDateString('en-US', { month: 'short', day: 'numeric', year: 'numeric' });

        // Update icon day number
        var svgText = trigger.querySelector('svg text');
        if (svgText) svgText.textContent = date.getDate();

        var label = trigger.querySelector('.popup-cal-trigger-label');
        if (label) label.textContent = formatted;
    }

    // Update selected day styling
    panel.querySelectorAll('.popup-cal-day--selected').forEach(function(d) {
        d.classList.remove('popup-cal-day--selected');
    });
    day.classList.add('popup-cal-day--selected');

    // Close panel
    panel.classList.remove('popup-cal-open');
});

// Popup Calendar - month navigation
document.addEventListener('click', function(e) {
    var isPrev = e.target.closest('[data-popup-cal-prev]');
    var isNext = e.target.closest('[data-popup-cal-next]');
    if (!isPrev && !isNext) return;

    e.stopPropagation();

    var btn = isPrev || isNext;
    var wrapperId = btn.getAttribute(isPrev ? 'data-popup-cal-prev' : 'data-popup-cal-next');
    var panel = document.getElementById(wrapperId + '-panel');
    if (!panel) return;

    // Get current month
    var monthStr = panel.getAttribute('data-month');
    var parts = monthStr.split('-');
    var year = parseInt(parts[0]);
    var month = parseInt(parts[1]) - 1; // JS months are 0-indexed
    var current = new Date(year, month, 1);

    // Calculate new month
    var newMonth = isPrev
        ? new Date(current.getFullYear(), current.getMonth() - 1, 1)
        : new Date(current.getFullYear(), current.getMonth() + 1, 1);

    // Update panel data-month
    var newMonthStr = newMonth.getFullYear() + '-' + String(newMonth.getMonth() + 1).padStart(2, '0');
    panel.setAttribute('data-month', newMonthStr);

    // Update hidden month field
    var monthHidden = document.getElementById(wrapperId + '-month');
    if (monthHidden) monthHidden.value = newMonthStr;

    // Update month label
    var monthLabel = panel.querySelector('.popup-cal-month-label');
    if (monthLabel) {
        var monthNames = ['January','February','March','April','May','June','July','August','September','October','November','December'];
        monthLabel.textContent = monthNames[newMonth.getMonth()] + ' ' + newMonth.getFullYear();
    }

    // Get selected date from hidden field
    var selectedHidden = document.getElementById(wrapperId + '-selected');
    var selectedDateStr = selectedHidden ? selectedHidden.value : '';

    // Re-render calendar grid
    var grid = panel.querySelector('.popup-cal-grid');
    if (!grid) return;
    grid.innerHTML = '';

    var firstOfMonth = new Date(newMonth.getFullYear(), newMonth.getMonth(), 1);
    var daysInMonth = new Date(newMonth.getFullYear(), newMonth.getMonth() + 1, 0).getDate();
    var startDayOfWeek = firstOfMonth.getDay();
    var today = new Date();
    today.setHours(0, 0, 0, 0);

    // Empty cells
    for (var i = 0; i < startDayOfWeek; i++) {
        var empty = document.createElement('span');
        empty.className = 'popup-cal-day popup-cal-day--empty';
        grid.appendChild(empty);
    }

    // Day buttons
    for (var day = 1; day <= daysInMonth; day++) {
        var date = new Date(newMonth.getFullYear(), newMonth.getMonth(), day);
        var dateStr = date.getFullYear() + '-' + String(date.getMonth() + 1).padStart(2, '0') + '-' + String(date.getDate()).padStart(2, '0');

        var classes = 'popup-cal-day';
        if (date.getTime() === today.getTime()) classes += ' popup-cal-day--today';
        if (selectedDateStr === dateStr) classes += ' popup-cal-day--selected';

        var dayBtn = document.createElement('button');
        dayBtn.type = 'button';
        dayBtn.className = classes;
        dayBtn.setAttribute('data-popup-cal-date', dateStr);
        dayBtn.setAttribute('data-popup-cal-target', wrapperId + '-selected');
        dayBtn.textContent = day;
        grid.appendChild(dayBtn);
    }
});

// Popup Calendar - Range selection
(function() {
    var rangeState = {}; // keyed by wrapperId

    function getRangeState(wrapperId) {
        if (!rangeState[wrapperId]) {
            rangeState[wrapperId] = { awaiting: 'start', start: null };
        }
        return rangeState[wrapperId];
    }

    function clearRangePreview(panel) {
        panel.querySelectorAll('.popup-cal-day--range-preview').forEach(function(d) {
            d.classList.remove('popup-cal-day--range-preview');
        });
    }

    function updateRangeHighlight(panel, startStr, endStr) {
        panel.querySelectorAll('.popup-cal-day').forEach(function(d) {
            d.classList.remove('popup-cal-day--range-start', 'popup-cal-day--range-end', 'popup-cal-day--in-range', 'popup-cal-day--range-preview');
            var dateStr = d.getAttribute('data-popup-cal-date');
            if (!dateStr) return;
            if (dateStr === startStr) d.classList.add('popup-cal-day--range-start');
            if (dateStr === endStr) d.classList.add('popup-cal-day--range-end');
            if (startStr && endStr && dateStr > startStr && dateStr < endStr)
                d.classList.add('popup-cal-day--in-range');
        });
    }

    function updateTriggerLabel(wrapperId, startStr, endStr) {
        var trigger = document.querySelector('[data-popup-cal="' + wrapperId + '-panel"]');
        if (!trigger) return;
        var label = trigger.querySelector('.popup-cal-trigger-label');
        if (!label) return;

        if (startStr && endStr) {
            var s = new Date(startStr + 'T00:00:00');
            var e = new Date(endStr + 'T00:00:00');
            label.textContent = s.toLocaleDateString('en-US', { month: 'short', day: 'numeric' }) + ' – ' + e.toLocaleDateString('en-US', { month: 'short', day: 'numeric', year: 'numeric' });
        } else if (startStr) {
            var s = new Date(startStr + 'T00:00:00');
            label.textContent = s.toLocaleDateString('en-US', { month: 'short', day: 'numeric', year: 'numeric' }) + ' – Select End';
        }
    }

    // Range day click
    document.addEventListener('click', function(e) {
        var day = e.target.closest('[data-popup-cal-date]');
        if (!day) return;

        var panel = day.closest('.popup-cal-panel');
        if (!panel) return;

        var wrapperId = panel.id.replace('-panel', '');
        var wrapper = document.getElementById(wrapperId);
        if (!wrapper || wrapper.getAttribute('data-mode') !== 'range') return;

        e.stopPropagation();

        if (day.classList.contains('popup-cal-day--disabled')) return;

        var dateVal = day.getAttribute('data-popup-cal-date');
        var state = getRangeState(wrapperId);

        if (state.awaiting === 'start') {
            // Set start date
            state.start = dateVal;
            state.awaiting = 'end';

            document.getElementById(wrapperId + '-start').value = dateVal;
            document.getElementById(wrapperId + '-end').value = '';

            updateRangeHighlight(panel, dateVal, null);
        } else {
            // Set end date
            var startVal = state.start;
            var endVal = dateVal;

            // Swap if end is before start
            if (endVal < startVal) {
                var temp = startVal;
                startVal = endVal;
                endVal = temp;
            }

            document.getElementById(wrapperId + '-start').value = startVal;
            document.getElementById(wrapperId + '-end').value = endVal;

            updateRangeHighlight(panel, startVal, endVal);
            updateTriggerLabel(wrapperId, startVal, endVal);

            state.start = null;
            state.awaiting = 'start';

            // Close panel
            panel.classList.remove('popup-cal-open');
        }
    });

    // Range hover preview
    document.addEventListener('mouseover', function(e) {
        var day = e.target.closest('[data-popup-cal-date]');
        if (!day) return;

        var panel = day.closest('.popup-cal-panel');
        if (!panel) return;

        var wrapperId = panel.id.replace('-panel', '');
        var wrapper = document.getElementById(wrapperId);
        if (!wrapper || wrapper.getAttribute('data-mode') !== 'range') return;

        var state = getRangeState(wrapperId);
        if (state.awaiting !== 'end' || !state.start) return;

        var hoverDate = day.getAttribute('data-popup-cal-date');
        clearRangePreview(panel);

        panel.querySelectorAll('.popup-cal-day').forEach(function(d) {
            var dateStr = d.getAttribute('data-popup-cal-date');
            if (!dateStr) return;
            if (hoverDate > state.start && dateStr > state.start && dateStr < hoverDate)
                d.classList.add('popup-cal-day--range-preview');
            if (hoverDate < state.start && dateStr < state.start && dateStr > hoverDate)
                d.classList.add('popup-cal-day--range-preview');
        });
    });
})();

// Rich Text Editor
document.querySelectorAll('.rte-btn').forEach(function(btn) {
    btn.addEventListener('click', function(e) {
        e.preventDefault();
        var cmd = btn.getAttribute('data-rte-cmd');
        var prompt = btn.getAttribute('data-rte-prompt');

        if (cmd === 'createLink') {
            var url = window.prompt(prompt || 'Enter URL:');
            if (url) document.execCommand(cmd, false, url);
        } else {
            document.execCommand(cmd, false, null);
        }

        // Update active states
        updateRteActiveStates(btn.closest('.rte-wrapper'));

        // Sync hidden input
        syncRteValue(btn.closest('.rte-wrapper'));
    });
});

document.querySelectorAll('.rte-body').forEach(function(editor) {
    editor.addEventListener('input', function() {
        syncRteValue(editor.closest('.rte-wrapper'));
    });
    editor.addEventListener('keyup', function() {
        updateRteActiveStates(editor.closest('.rte-wrapper'));
    });
    editor.addEventListener('mouseup', function() {
        updateRteActiveStates(editor.closest('.rte-wrapper'));
    });
});

function syncRteValue(wrapper) {
    if (!wrapper) return;
    var editor = wrapper.querySelector('.rte-body');
    var hidden = wrapper.querySelector('input[type="hidden"]');
    if (editor && hidden) hidden.value = editor.innerHTML;
}

function updateRteActiveStates(wrapper) {
    if (!wrapper) return;
    wrapper.querySelectorAll('.rte-btn[data-rte-cmd]').forEach(function(btn) {
        var cmd = btn.getAttribute('data-rte-cmd');
        try {
            if (document.queryCommandState(cmd)) {
                btn.classList.add('active');
            } else {
                btn.classList.remove('active');
            }
        } catch(e) {}
    });
}

// Emoji picker toggle
document.addEventListener('click', function (e) {
    const emojiBtn = e.target.closest('[data-rte-emoji-panel]');
    if (emojiBtn) {
        e.stopPropagation();
        const panelId = emojiBtn.getAttribute('data-rte-emoji-panel');
        const panel = document.getElementById(panelId);
        if (panel) {
            const isVisible = panel.style.display !== 'none';
            document.querySelectorAll('.rte-emoji-panel').forEach(p => p.style.display = 'none');
            panel.style.display = isVisible ? 'none' : 'block';
            if (!isVisible) {
                const rect = emojiBtn.getBoundingClientRect();
                const wrapperRect = emojiBtn.closest('.rte-wrapper').getBoundingClientRect();
                const btnRect = emojiBtn.getBoundingClientRect();
                panel.style.top = (btnRect.bottom - wrapperRect.top + 4) + 'px';
                panel.style.left = (btnRect.left - wrapperRect.left) + 'px';
            }
        }
        return;
    }

    // Emoji insert
    const emojiItem = e.target.closest('[data-emoji]');
    if (emojiItem) {
        const emoji = emojiItem.getAttribute('data-emoji');
        const editorId = emojiItem.getAttribute('data-rte-editor');
        const editor = document.getElementById(editorId);
        if (editor) {
            editor.focus();
            document.execCommand('insertText', false, emoji);
        }
        const panel = emojiItem.closest('.rte-emoji-panel');
        if (panel) panel.style.display = 'none';
        return;
    }

    // Close emoji panels on outside click
    document.querySelectorAll('.rte-emoji-panel').forEach(p => p.style.display = 'none');
});