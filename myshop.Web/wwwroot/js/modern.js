/* ============================================
   Modern JS - ShopHub
   Dropdowns, Sidebar Toggle, Interactions
   ============================================ */

(function () {
  'use strict';

  // --- Hamburger Menu Toggle ---
  var hamburger = document.getElementById('siteHamburger');
  var siteNav = document.getElementById('siteNav');
  var overlay = null;

  function createOverlay() {
    overlay = document.createElement('div');
    overlay.className = 'site-nav-overlay show';
    document.body.appendChild(overlay);
    overlay.addEventListener('click', closeMobileNav);
  }

  function removeOverlay() {
    if (overlay) {
      overlay.remove();
      overlay = null;
    }
  }

  function closeMobileNav() {
    if (hamburger) hamburger.classList.remove('active');
    if (siteNav) siteNav.classList.remove('open');
    removeOverlay();
    document.body.style.overflow = '';
  }

  if (hamburger && siteNav) {
    hamburger.addEventListener('click', function (e) {
      e.preventDefault();
      var isOpen = siteNav.classList.contains('open');
      if (isOpen) {
        closeMobileNav();
      } else {
        hamburger.classList.add('active');
        siteNav.classList.add('open');
        createOverlay();
        document.body.style.overflow = 'hidden';
      }
    });

    // Close on Escape key
    document.addEventListener('keydown', function (e) {
      if (e.key === 'Escape' && siteNav.classList.contains('open')) {
        closeMobileNav();
      }
    });
  }

  // --- Dropdown ---
  document.addEventListener('click', function (e) {
    // Close all open dropdowns when clicking outside
    var dropdowns = document.querySelectorAll('.dropdown-menu.show');
    for (var i = 0; i < dropdowns.length; i++) {
      var dropdown = dropdowns[i];
      if (!dropdown.parentElement.contains(e.target)) {
        dropdown.classList.remove('show');
      }
    }

    // Toggle dropdown on click
    var toggleBtn = e.target.closest('.dropdown-toggle');
    if (toggleBtn) {
      e.preventDefault();
      e.stopPropagation();
      var menu = toggleBtn.nextElementSibling;
      if (!menu || !menu.classList.contains('dropdown-menu')) {
        menu = toggleBtn.parentElement.querySelector('.dropdown-menu');
      }
      if (menu) {
        var isOpen = menu.classList.contains('show');
        // Close all other dropdowns first
        var allMenus = document.querySelectorAll('.dropdown-menu.show');
        for (var j = 0; j < allMenus.length; j++) {
          allMenus[j].classList.remove('show');
        }
        if (!isOpen) {
          menu.classList.add('show');
        }
      }
    }
  });

  // --- Sidebar Toggle (Dashboard) ---
  document.addEventListener('click', function (e) {
    var toggle = e.target.closest('[data-widget="pushmenu"], .sidebar-toggle');
    if (toggle) {
      e.preventDefault();
      var sidebar = document.querySelector('.dashboard-sidebar');
      var wrapper = document.querySelector('.dashboard-wrapper');
      if (sidebar) {
        sidebar.classList.toggle('collapsed');
        sidebar.classList.toggle('mobile-open');
      }
      if (wrapper) {
        wrapper.classList.toggle('sidebar-collapsed');
      }
    }
  });

  // --- Card Collapse/Remove (Dashboard) ---
  document.addEventListener('click', function (e) {
    var cardWidget = e.target.closest('[data-card-widget]');
    if (cardWidget) {
      e.preventDefault();
      var action = cardWidget.getAttribute('data-card-widget');
      var card = cardWidget.closest('.card, .dashboard-card');
      if (!card) return;

      if (action === 'collapse') {
        var body = card.querySelector('.card-body, .dashboard-card-body');
        if (body) {
          var isHidden = body.style.display === 'none';
          body.style.display = isHidden ? '' : 'none';
          var icon = cardWidget.querySelector('i');
          if (icon) {
            icon.classList.toggle('fa-minus', !isHidden);
            icon.classList.toggle('fa-plus', isHidden);
          }
        }
      } else if (action === 'remove') {
        card.style.transition = 'opacity 0.3s, transform 0.3s';
        card.style.opacity = '0';
        card.style.transform = 'scale(0.95)';
        setTimeout(function () { card.remove(); }, 300);
      }
    }
  });

  // --- Mobile sidebar overlay close ---
  document.addEventListener('click', function (e) {
    var sidebar = document.querySelector('.dashboard-sidebar');
    if (sidebar && sidebar.classList.contains('mobile-open')) {
      if (!sidebar.contains(e.target) && !e.target.closest('.sidebar-toggle')) {
        sidebar.classList.remove('mobile-open');
        var wrapper = document.querySelector('.dashboard-wrapper');
        if (wrapper) wrapper.classList.remove('sidebar-collapsed');
      }
    }
  });

  // --- Smooth scroll for anchor links ---
  document.querySelectorAll('a[href^="#"]').forEach(function (anchor) {
    anchor.addEventListener('click', function (e) {
      var targetId = this.getAttribute('href');
      if (targetId === '#') return;
      var target = document.querySelector(targetId);
      if (target) {
        e.preventDefault();
        target.scrollIntoView({ behavior: 'smooth', block: 'start' });
      }
    });
  });

  // --- Form validation visual feedback ---
  document.querySelectorAll('.form-control').forEach(function (input) {
    input.addEventListener('invalid', function () {
      this.classList.add('is-invalid');
    });
    input.addEventListener('input', function () {
      if (this.classList.contains('is-invalid')) {
        if (this.checkValidity()) {
          this.classList.remove('is-invalid');
          this.classList.add('is-valid');
        }
      }
    });
  });

})();
