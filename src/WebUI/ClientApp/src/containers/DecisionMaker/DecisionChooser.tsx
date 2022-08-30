import { useCallback, useEffect, type FunctionComponent } from 'react';
import { useNavigate } from 'react-router-dom';
import { toast } from 'react-toastify';
import { useAppSelector, useAppDispatch } from '../../store';
import { getDecisionByIdAsync, getFirstDecisionAsync, type Decision } from '../../store/decisionSlice';

type DecisionChooserProps = { treeName: string };

const DecisionChooser: FunctionComponent<DecisionChooserProps> = ({ treeName }) => {
    const dispatch = useAppDispatch();
    const currentDecision = useAppSelector<Decision | null>((state) => state.decision.currentDecision);
    const isResult = !!currentDecision?.result;
    const navigate = useNavigate();

    useEffect(() => {
        dispatch(getFirstDecisionAsync(treeName));
    }, [treeName]);

    useEffect(() => {
        if (isResult)
            toast.success("Congratulations with finding your decision!");
    }, [isResult]);

    const showDecisionTreeHandler = useCallback(() => {
        navigate(`/decisionTreeResult/${treeName}`);
    }, [treeName])

    return (
        <div className="is-flex is-flex-direction-column is-align-items-center">
            {isResult && <><h1 className="title is-4">{currentDecision?.result}</h1>
                <button
                    className="button is-warning is-large"
                    onClick={showDecisionTreeHandler}>
                    Show decision tree
                </button>
            </>}
            {!isResult && <>
                <h2 className="title is-4">
                    {currentDecision?.question}
                </h2>
                <div> {currentDecision?.possibleAnswers.map((p, i) => (
                    <button
                        key={i}
                        className="button is-warning is-large"
                        onClick={() => dispatch(getDecisionByIdAsync(p.id))} style={{ margin: "20px" }}>
                        {p.answer}
                    </button>
                ))}
                </div>
            </>}
        </div>
    );
};

export default DecisionChooser;