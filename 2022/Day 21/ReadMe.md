<main>
<article class="day-desc"><h2>--- Day 21: Monkey Math ---</h2><p>The <a href="11">monkeys</a> are back! You're worried they're going to try to steal your stuff again, but it seems like they're just holding their ground and making various monkey noises at you.</p>
<p>Eventually, one of the elephants realizes you don't speak monkey and comes over to interpret. As it turns out, they overheard you talking about trying to find the grove; they can show you a shortcut if you answer their <em>riddle</em>.</p>
<p>Each monkey is given a <em>job</em>: either to <em>yell a specific number</em> or to <em>yell the result of a math operation</em>. All of the number-yelling monkeys know their number from the start; however, the math operation monkeys need to wait for two other monkeys to yell a number, and those two other monkeys might <em>also</em> be waiting on other monkeys.</p>
<p>Your job is to <em>work out the number the monkey named <code>root</code> will yell</em> before the monkeys figure it out themselves.</p>
<p>For example:</p>
<pre><code>root: pppw + sjmn
dbpl: 5
cczh: sllz + lgvd
zczc: 2
ptdq: humn - dvpt
dvpt: 3
lfqf: 4
humn: 5
ljgn: 2
sjmn: drzm * dbpl
sllz: 4
pppw: cczh / lfqf
lgvd: ljgn * ptdq
drzm: hmdt - zczc
hmdt: 32
</code></pre>
<p>Each line contains the name of a monkey, a colon, and then the job of that monkey:</p>
<ul>
<li>A lone number means the monkey's job is simply to yell that number.</li>
<li>A job like <code>aaaa + bbbb</code> means the monkey waits for monkeys <code>aaaa</code> and <code>bbbb</code> to yell each of their numbers; the monkey then yells the sum of those two numbers.</li>
<li><code>aaaa - bbbb</code> means the monkey yells <code>aaaa</code>'s number minus <code>bbbb</code>'s number.</li>
<li>Job <code>aaaa * bbbb</code> will yell <code>aaaa</code>'s number multiplied by <code>bbbb</code>'s number.</li>
<li>Job <code>aaaa / bbbb</code> will yell <code>aaaa</code>'s number divided by <code>bbbb</code>'s number.</li>
</ul>
<p>So, in the above example, monkey <code>drzm</code> has to wait for monkeys <code>hmdt</code> and <code>zczc</code> to yell their numbers. Fortunately, both <code>hmdt</code> and <code>zczc</code> have jobs that involve simply yelling a single number, so they do this immediately: <code>32</code> and <code>2</code>. Monkey <code>drzm</code> can then yell its number by finding <code>32</code> minus <code>2</code>: <code><em>30</em></code>.</p>
<p>Then, monkey <code>sjmn</code> has one of its numbers (<code>30</code>, from monkey <code>drzm</code>), and already has its other number, <code>5</code>, from <code>dbpl</code>. This allows it to yell its own number by finding <code>30</code> multiplied by <code>5</code>: <code><em>150</em></code>.</p>
<p>This process continues until <code>root</code> yells a number: <code><em>152</em></code>.</p>
<p>However, your actual situation involves <span title="Advent of Code 2022: Now With Considerably More Monkeys">considerably more monkeys</span>. <em>What number will the monkey named <code>root</code> yell?</em></p>
</article>
</main>