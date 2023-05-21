import './HomeStyle.css';

const TrendBox = () => {


    return (
        <div className="hide">
            <aside className="trendBox">
                <h2 className="trendFont">Trending Topics</h2>
                <ul>
                    <li>#trending1</li>
                    <li>#trending2</li>
                    <li>#trending3</li>
                    <li>#trending4</li>
                    <li>#trending5</li>
                </ul>
            </aside>
            <footer className="homeFooter">
                <p>&copy; 2069 Twitter</p>
            </footer>
        </div>
        )
}
export default TrendBox;