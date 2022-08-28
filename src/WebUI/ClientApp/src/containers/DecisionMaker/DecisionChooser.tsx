import { useEffect, type FunctionComponent } from 'react';
import { useNavigate } from 'react-router-dom';
import { Spinner } from 'src/components';
import { getDecisionTreeAsync } from 'src/store/decisionTreeSlice';
import { useAppSelector, useAppDispatch } from '../../store';
import { getDecisionByIdAsync, getFirstDecisionAsync, type Decision } from '../../store/decisionSlice';

type DecisionChooserProps = { treeName: string };

const DecisionChooser: FunctionComponent<DecisionChooserProps> = ({ treeName }) => {
    const dispatch = useAppDispatch();
    const currentDecision = useAppSelector<Decision | null>((state) => state.decision.currentDecision);
    const isLoading = useAppSelector<boolean>((state) => state.decision.isLoading);
    const isResult = currentDecision?.result !== null;// TODO add congratulations toast
    const navigate = useNavigate();

    useEffect(() => {
        dispatch(getFirstDecisionAsync(treeName)); //mb move back to index
    }, [dispatch, treeName]);

    const showDecisionTreeHandler = () => {// TODO mb useCallback
        dispatch(getDecisionTreeAsync(treeName));
        navigate(`/decisionTreeResult/${treeName}`);
    }

    return (
        <>
            <Spinner isLoading={isLoading} />
            {!isLoading &&
                <div>
                    {isResult && <><h3 className="title is-4">{currentDecision?.result}</h3>
                        <button
                            className="button is-info"
                            onClick={() => showDecisionTreeHandler()}>
                            Show decision tree
                        </button>
                    </>}
                    {!isResult && <>
                        <h3 className="title is-4">
                            {currentDecision?.question}
                        </h3>
                        {currentDecision?.possibleAnswers.map((p, i) => (
                            <button
                                key={i}
                                className="button is-info"
                                onClick={() => dispatch(getDecisionByIdAsync(p.id))}>
                                {p.answer}
                            </button>
                        ))}
                    </>}
                </div>}
        </>
    );
};

export default DecisionChooser;
