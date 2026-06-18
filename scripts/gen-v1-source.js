// @ts-check
const fs = require('fs')
const path = require('path')

/** @param {string} dir @param {string} ext @returns {string[]} */
function walk(dir, ext) {
    const results = []
    for (const entry of fs.readdirSync(dir, { withFileTypes: true })) {
        const full = path.join(dir, entry.name)
        if (entry.isDirectory()) results.push(...walk(full, ext))
        else if (entry.name.endsWith(ext)) results.push(full)
    }
    return results
}

const files = walk('src/App', '.fs')
const out = {}
files.forEach(f => { out[f.replace(/\\/g, '/')] = fs.readFileSync(f, 'utf8') })
fs.mkdirSync('public', { recursive: true })
fs.writeFileSync('public/v1-source.json', JSON.stringify(out))
console.log(`v1-source.json: ${files.length} files`)
