/*!
 * Color mode toggler for Bootstrap's docs (https://getbootstrap.com/)
 * Copyright 2011-2022 The Bootstrap Authors
 * Licensed under the Creative Commons Attribution 3.0 Unported License.
 */

(() => {
    'use strict'

    const storedTheme = localStorage.getItem('theme')

    const getPreferredTheme = () => {
        if (storedTheme) {
            return storedTheme
        }
        return 'auto'
    }

    const setTheme = function (theme) {
        // deal with 'auto' + 'dark' theme
        if (theme === 'auto' && window.matchMedia('(prefers-color-scheme: dark)').matches) {
            document.documentElement.setAttribute('data-bs-theme', 'dark');
            document.querySelectorAll('#light-svg, #dark-svg, #auto-svg').forEach(function (elem) {
                elem.classList.add('invert-svg-color');
            });
            return;
        }
        document.documentElement.setAttribute('data-bs-theme', theme);
        // deal with 'dark' theme
        if (theme === 'dark') {
            document.querySelectorAll('#light-svg, #dark-svg, #auto-svg').forEach(function (elem) {
                elem.classList.add('invert-svg-color');
            });
        }
        // deal with 'light' or 'auto' + 'light' theme
        else {
            document.querySelectorAll('#light-svg, #dark-svg, #auto-svg').forEach(function (elem) {
                elem.classList.remove('invert-svg-color');
            });
        }
    }

    setTheme(getPreferredTheme())

    const showActiveTheme = theme => {
        const activeThemeIcon = document.querySelector('.theme-icon-active use')
        const btnToActive = document.querySelector(`[data-bs-theme-value="${theme}"]`)
        const svgOfActiveBtn = btnToActive.querySelector('svg use').getAttribute('href')

        document.querySelectorAll('[data-bs-theme-value]').forEach(element => {
            element.classList.remove('active')
        })

        btnToActive.classList.add('active')
        activeThemeIcon.setAttribute('href', svgOfActiveBtn)
    }

    window.matchMedia('(prefers-color-scheme: dark)').addEventListener('change', () => {
        if (storedTheme !== 'light' || storedTheme !== 'dark') {
            setTheme(getPreferredTheme())
        }
    })

    window.addEventListener('DOMContentLoaded', () => {
        showActiveTheme(getPreferredTheme())

        document.querySelectorAll('[data-bs-theme-value]')
            .forEach(toggle => {
                toggle.addEventListener('click', () => {
                    const theme = toggle.getAttribute('data-bs-theme-value')
                    localStorage.setItem('theme', theme)
                    setTheme(theme)
                    showActiveTheme(theme)
                })
            })
    })
})()