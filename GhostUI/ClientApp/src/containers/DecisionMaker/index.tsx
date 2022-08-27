import { Link, useParams } from 'react-router-dom';
import { useEffect, type FunctionComponent } from 'react';
import { useAppDispatch, useAppSelector } from '../../store';
import DecisionChooser from './DecisionChooser';
import { getDecisionTreesAsync } from 'src/store/decisionTreesSlice';

const DecisionMaker: FunctionComponent = () => {
    const dispatch = useAppDispatch();
    const decisionTreesNames = useAppSelector<string[]>((state) => state.decisionTrees.decisionTreesNames);
    const { treeName } = useParams();

    useEffect(() => {
        dispatch(getDecisionTreesAsync());
    }, [dispatch]);

    return (
        <div className="section">
            <div className="container">
                <h3 className="title is-3">
                    Choose your journey
                </h3>
                {!treeName && decisionTreesNames.map((decisionTreesName, i) =>
                    <Link key={i} to={`/decisionTrees/${decisionTreesName}`}>
                        <span>{decisionTreesName}</span>{/* TODO add Icon or Image */}
                    </Link>
                )}

                {treeName && <div className="box container-box">
                    <DecisionChooser treeName={treeName} />
                </div>}
            </div>
        </div>
    );
};

export default DecisionMaker;
