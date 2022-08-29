import { Link, useParams } from 'react-router-dom';
import { useEffect, useLayoutEffect, type FunctionComponent } from 'react';
import { useAppDispatch, useAppSelector } from '../../store';
import DecisionChooser from './DecisionChooser';
import { getDecisionTreesAsync } from 'src/store/decisionTreesSlice';
import styled from 'styled-components';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { clearCurrentDecision } from 'src/store/decisionSlice';

const StyledDiv = styled.div`
  display: grid;
  grid-template-columns: 1fr 1fr 1fr;
`;

const DecisionMaker: FunctionComponent = () => {
    const dispatch = useAppDispatch();
    const decisionTreesNames = useAppSelector<string[]>((state) => state.decisionTrees.decisionTreesNames);
    const { treeName } = useParams();

    dispatch(clearCurrentDecision());

    useEffect(() => {
        dispatch(getDecisionTreesAsync());
    }, [dispatch]);

    return (
        <div className="section">
            <div className="container">
                <h3 className="title is-3">
                    Going through your journey
                </h3>
                <StyledDiv>
                    {!treeName && decisionTreesNames.map((decisionTreesName, i) =>
                        <Link key={i} to={`/decisionTrees/${decisionTreesName}`}>
                            <div className="column">
                                <div className="card">
                                    <div className="card-image">
                                        <figure className="image is-4by3">
                                            {/* <img src="https://i.pinimg.com/originals/8d/5e/e2/8d5ee22807e384f09cd6bb7704432860.jpg" alt="Placeholder image" /> */}
                                            {/* <FontAwesomeIcon icon={'tree'} /> */}
                                            {/* <FontAwesomeIcon icon={"fa-tree"} /> */}
                                            {/* <FontAwesomeIcon icon={['fas', 'tree']} /> */}
                                            123
                                        </figure>
                                    </div>
                                    <div className="card-content">
                                        <div className="media">
                                            <div className="media-content">
                                                <p className="title is-4">{decisionTreesName}</p>
                                                <p className="subtitle is-6">Decision tree depth</p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </Link>
                    )}
                </StyledDiv>

                {treeName && <div className="box container-box is-flex is-flex-direction-column is-align-items-center is-justify-content-center">
                    <DecisionChooser treeName={treeName} />
                </div>}
            </div>
        </div>
    );
};

export default DecisionMaker;