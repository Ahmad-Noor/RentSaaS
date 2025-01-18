/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./src/**/*.{html,ts}",
     "./node_modules/flowbite/**/*.js"
  ],
  theme: {
    extend: {
      colors: {
        primary: '#4B8FD9',
        secondary: '#1B3A5C',
      }
    },
  },

  plugins: [
    require('flowbite/plugin')
]
}