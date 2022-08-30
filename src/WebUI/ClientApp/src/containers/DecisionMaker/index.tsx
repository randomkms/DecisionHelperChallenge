import { Link, useParams } from 'react-router-dom';
import { useEffect, type FunctionComponent } from 'react';
import { useAppDispatch, useAppSelector } from '../../store';
import DecisionChooser from './DecisionChooser';
import { getDecisionTreesAsync, type DecisionTreeInfo } from 'src/store/decisionTreesSlice';
import styled from 'styled-components';
import { clearCurrentDecision } from 'src/store/decisionSlice';

const StyledDiv = styled.div`
  display: grid;
  grid-template-columns: 1fr 1fr 1fr;
`;

const DecisionMaker: FunctionComponent = () => {
    const dispatch = useAppDispatch();
    const decisionTreesInfos = useAppSelector<DecisionTreeInfo[]>((state) => state.decisionTrees.decisionTreesInfos);
    const { treeName } = useParams();

    useEffect(() => {
        dispatch(clearCurrentDecision());
    }, [treeName]);

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
                    {!treeName && decisionTreesInfos.map((decisionTreeInfo, i) =>
                        <Link key={i} to={`/decisionTrees/${decisionTreeInfo.name}`}>
                            <div className="column">
                                <div className="card">
                                    <div className="card-image">
                                        <figure className="image is-4by3">
                                            <img src={decisionTreeInfo.imageUrl} alt="Placeholder image" />
                                        </figure>
                                    </div>
                                    <div className="card-content">
                                        <div className="media">
                                            <div className="media-content">
                                                <p className="title is-4">{decisionTreeInfo.name}</p>
                                                <p className="subtitle is-6">{decisionTreeInfo.description}</p>
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